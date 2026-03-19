import hashlib
import math
import random
import datetime
import unittest
import matplotlib.pyplot as plt

# Вариант 3

class CountMinSketch:
    def __init__(self, epsilon=None, delta=0.01, w=None, d=None, hash_func=hashlib.sha256):
        if w is None and epsilon is not None:
            w = math.ceil(math.e / epsilon)

        if d is None and delta is not None:
            d = math.ceil(math.log(1 / delta))

        self.w = w
        self.d = d
        self.epsilon = epsilon
        self.delta = delta
        self.hash_func = hash_func

        self.C = [[0] * self.w for _ in range(self.d)]
        self.seeds = [i * 31 + 7 for i in range(self.d)]

    def hash(self, item, i):
        h = int(self.hash_func((str(item) + str(self.seeds[i])).encode()).hexdigest(), 16)
        return h % self.w

    def add(self, item, count=1):
        for i in range(self.d):
            j = self.hash(item, i)
            self.C[i][j] += count

    def estimate(self, item):
        return min(self.C[i][self.hash(item, i)] for i in range(self.d))

    def __add__(self, other):
        if self.w != other.w or self.d != other.d or self.hash_func != other.hash_func:
            raise ValueError("CMS must have same parameters and hash function")
        result = CountMinSketch(w=self.w, d=self.d, hash_func=self.hash_func)
        for i in range(self.d):
            for j in range(self.w):
                result.C[i][j] = self.C[i][j] + other.C[i][j]
        return result

def stream_dates():
    while True:
        yield datetime.datetime(
            random.randint(1900, 2030),
            random.randint(1, 12),
            random.randint(1, 28),
        )

STREAM_SIZES = {"norm": 250_000, "big": 1_150_000}
EPSILONS = [0.01]


class TestCountMinSketch(unittest.TestCase):

    def run_stream_test(self, n_elements, epsilon):
        cms = CountMinSketch(epsilon=epsilon)
        stream = stream_dates()
        counts = {}

        for i, item in enumerate(stream):
            if i >= n_elements:
                break
            counts[item] = counts.get(item, 0) + 1
            cms.add(item)

        sample_items = list(counts.keys())[:100]
        errors = []

        for item in sample_items:
            est = cms.estimate(item)
            exact = counts[item]
            abs_err = abs(est - exact)
            rel_err = abs_err / exact * 100
            errors.append((item, exact, est, abs_err, rel_err))

        return errors

    def test_error_cms(self):
        for eps in [0.01]:
            for size_name, n in STREAM_SIZES.items():
                errors = self.run_stream_test(n, eps)

                max_rel_err = max(r[4] for r in errors)
                max_abs_err = max(r[3] for r in errors)

                print(
                    f"{size_name} stream | epsilon={eps} | "
                    f"max_abs_error={max_abs_err:.2f} | "
                    f"max_rel_error={max_rel_err:.2f}%"
                )

                max_allowed_error = eps * n

                for r in errors:
                    self.assertLessEqual(r[3], max_allowed_error)

    def test_union_cms(self):
        cms1 = CountMinSketch(epsilon=0.05)
        cms2 = CountMinSketch(epsilon=0.05)

        stream1 = stream_dates()
        stream2 = stream_dates()

        counts = {}

        for i, item in enumerate(stream1):
            if i >= 100_000:
                break
            counts[item] = counts.get(item, 0) + 1
            cms1.add(item)

        for i, item in enumerate(stream2):
            if i >= 150_000:
                break
            counts[item] = counts.get(item, 0) + 1
            cms2.add(item)

        cms_union = cms1 + cms2

        sample_items = list(counts.keys())[:100]
        errors = []

        for item in sample_items:
            est = cms_union.estimate(item)
            exact = counts[item]
            abs_err = abs(est - exact)
            rel_err = abs_err / exact * 100
            errors.append((item, exact, est, abs_err, rel_err))

        max_abs_err = max(r[3] for r in errors)
        max_rel_err = max(r[4] for r in errors)

        print(
            f"Union CMS | max_abs_error={max_abs_err:.2f} "
            f"| max_rel_error={max_rel_err:.2f}%"
        )

        max_allowed_error = 0.05 * (100_000 + 150_000)

        for r in errors:
            self.assertLessEqual(r[3], max_allowed_error)

STREAM_SIZES = {"norm": 250_000, "big": 1_150_000}
EPSILONS = [0.02, 0.01, 0.005]
DELTAS = [0.1, 0.01, 0.001]

def evaluate_hyperparameters():
    results = []

    for eps in EPSILONS:
        for delta in DELTAS:
            for size_name, n in STREAM_SIZES.items():

                cms = CountMinSketch(epsilon=eps, delta=delta)
                stream = stream_dates()
                counts = {}

                for i, item in enumerate(stream):
                    if i >= n:
                        break
                    counts[item] = counts.get(item, 0) + 1
                    cms.add(item)

                sample_items = list(counts.keys())[:100]

                abs_errors = []
                rel_errors = []

                for item in sample_items:
                    est = cms.estimate(item)
                    exact = counts[item]

                    abs_err = abs(est - exact)
                    rel_err = abs_err / exact * 100

                    abs_errors.append(abs_err)
                    rel_errors.append(rel_err)

                avg_abs = sum(abs_errors) / len(abs_errors)
                avg_rel = sum(rel_errors) / len(rel_errors)

                results.append({
                    "epsilon": eps,
                    "delta": delta,
                    "size": size_name,
                    "avg_abs": avg_abs,
                    "avg_rel": avg_rel
                })

    return results


def print_table(results):
    print(f"{'size':^20}{'epsilon':^20}{'delta':^20}{'avg_abs':^20}{'avg_rel (%)':^20}")
    for r in results:
        print(f"{r['size']:^20}{r['epsilon']:^20.6f}{r['delta']:^20.6f}"
              f"{r['avg_abs']:^20.2f}{r['avg_rel']:^20.2f}")
    print()

    with open("cms_results.txt", "w", encoding="utf-8") as f:
        f.write(f"{'size':^20}{'epsilon':^20}{'delta':^20}{'avg_abs':^20}{'avg_rel (%)':^20}\n")
        for r in results:
            f.write(f"{r['size']:^20}{r['epsilon']:^20.6f}{r['delta']:^20.6f}"
                    f"{r['avg_abs']:^20.2f}{r['avg_rel']:^20.2f}\n")

def plot_results(results):
    fixed_delta = 0.01

    plt.figure()

    for size_name in STREAM_SIZES:
        xs = []
        ys_abs = []

        for r in results:
            if r["delta"] == fixed_delta and r["size"] == size_name:
                xs.append(r["epsilon"])
                ys_abs.append(r["avg_abs"])

        pairs = sorted(zip(xs, ys_abs))
        xs, ys_abs = zip(*pairs)

        plt.plot(xs, ys_abs, marker='o', label=f"{size_name}")

    plt.xlabel("epsilon")
    plt.ylabel("Average absolute error")
    plt.title("Absolute error vs epsilon (delta = 0.01)")
    plt.legend()
    plt.grid(True)
    plt.savefig(f"cms_epsilon.png")
    plt.show()

    fixed_epsilon = 0.01

    plt.figure()

    for size_name in STREAM_SIZES:
        xs = []
        ys_abs = []

        for r in results:
            if r["epsilon"] == fixed_epsilon and r["size"] == size_name:
                xs.append(r["delta"])
                ys_abs.append(r["avg_abs"])

        pairs = sorted(zip(xs, ys_abs))
        xs, ys_abs = zip(*pairs)

        plt.plot(xs, ys_abs, marker='o', label=f"{size_name}")

    plt.xlabel("delta")
    plt.ylabel("Average absolute error")
    plt.title("Absolute error vs delta (epsilon = 0.01)")
    plt.legend()
    plt.grid(True)
    plt.savefig(f"cms_delta.png")
    plt.show()


if __name__ == "__main__":
    unittest.main()

results = evaluate_hyperparameters()
print_table(results)
plot_results(results)
