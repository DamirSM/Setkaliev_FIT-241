import hashlib
import math
import random
import datetime
import unittest
import matplotlib.pyplot as plt

# -----------------------------
# Генератор потока дат
# -----------------------------
def stream_dates():
    while True:
        yield datetime.datetime(
            random.randint(1900, 2030),
            random.randint(1, 12),
            random.randint(1, 28),
            random.randint(0, 23),
            random.randint(0, 59),
        )

# -----------------------------
# Count-Min Sketch
# -----------------------------
class CountMinSketch:

    def __init__(self, epsilon=None, delta=0.01, d=None, w=None):

        self.epsilon = epsilon
        self.delta = delta

        if w is None and epsilon is not None:
            self.w = math.ceil(math.e / epsilon)
        else:
            self.w = w

        if d is None:
            self.d = math.ceil(math.log(1 / delta))
        else:
            self.d = d

        self.C = [[0] * self.w for _ in range(self.d)]

        self.seeds = [i * 31 + 7 for i in range(self.d)]

    def _hash(self, item, i):
        h = hashlib.sha256((str(item) + str(self.seeds[i])).encode()).hexdigest()
        return int(h, 16) % self.w

    def add(self, item, count=1):
        for i in range(self.d):
            idx = self._hash(item, i)
            self.C[i][idx] += count

    def estimate(self, item):
        return min(self.C[i][self._hash(item, i)] for i in range(self.d))

    def __add__(self, other):

        if self.d != other.d or self.w != other.w:
            raise ValueError("CMS must have same dimensions to merge")

        result = CountMinSketch(d=self.d, w=self.w)

        for i in range(self.d):
            for j in range(self.w):
                result.C[i][j] = self.C[i][j] + other.C[i][j]

        return result


# -----------------------------
# Параметры
# -----------------------------
STREAM_SIZES = {"norm": 250_000, "big": 1_150_000}

EPSILONS = [0.005, 0.01, 0.02]
DELTAS = [0.1, 0.01, 0.001]


# -----------------------------
# Юнит тесты
# -----------------------------
class TestCountMinSketch(unittest.TestCase):

    def run_stream_test(self, n_elements, epsilon):

        cms = CountMinSketch(epsilon=epsilon)

        stream = stream_dates()

        counts = {}

        for _ in range(n_elements):

            item = next(stream)

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

        stream = stream_dates()

        counts = {}

        n1 = 100_000
        n2 = 150_000

        for _ in range(n1):

            item = next(stream)

            counts[item] = counts.get(item, 0) + 1

            cms1.add(item)

        for _ in range(n2):

            item = next(stream)

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

        max_allowed_error = 0.05 * (n1 + n2)

        for r in errors:
            self.assertLessEqual(r[3], max_allowed_error)


# -----------------------------
# Эксперимент для графиков
# -----------------------------
def evaluate_hyperparameters():

    results = []

    for eps in EPSILONS:

        for delta in DELTAS:

            for size_name, n in STREAM_SIZES.items():

                cms = CountMinSketch(epsilon=eps, delta=delta)

                stream = stream_dates()

                counts = {}

                for _ in range(n):

                    item = next(stream)

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

                print(
                    f"size={size_name} "
                    f"epsilon={eps} "
                    f"delta={delta} "
                    f"avg_abs={avg_abs:.2f} "
                    f"avg_rel={avg_rel:.2f}%"
                )

                results.append((eps, delta, size_name, avg_abs, avg_rel))

    return results


# -----------------------------
# Графики
# -----------------------------
def plot_results(results):

    # график абсолютной ошибки
    for size_name in STREAM_SIZES:

        xs = []
        ys = []

        for r in results:

            if r[2] == size_name:
                xs.append(r[0])
                ys.append(r[3])

        plt.plot(xs, ys, marker='o', label=f"{size_name}")

    plt.xlabel("epsilon")

    plt.ylabel("Average absolute error")

    plt.title("Absolute error vs epsilon")

    plt.legend()

    plt.grid(True)

    plt.show()


    # график относительной ошибки

    for size_name in STREAM_SIZES:

        xs = []
        ys = []

        for r in results:

            if r[2] == size_name:
                xs.append(r[0])
                ys.append(r[4])

        plt.plot(xs, ys, marker='o', label=f"{size_name}")

    plt.xlabel("epsilon")

    plt.ylabel("Average relative error (%)")

    plt.title("Relative error vs epsilon")

    plt.legend()

    plt.grid(True)

    plt.show()


# -----------------------------
# main
# -----------------------------
if __name__ == "__main__":

    unittest.main()

results = evaluate_hyperparameters()

plot_results(results)