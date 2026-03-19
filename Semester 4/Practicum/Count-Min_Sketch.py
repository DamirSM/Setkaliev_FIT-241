import hashlib
import math
import random
import datetime
import unittest
import matplotlib.pyplot as plt

#Вариант 3

class CountMinSketch:
    def __init__(self, epsilon=None, delta=0.001, w=None, d=None, hash_func=hashlib.sha256):
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
        h = self.hash_func((str(item) + str(self.seeds[i])).encode()).hexdigest()
        return int(h, 16) % self.w

    def add(self, item, count=1):
        for i in range(self.d):
            j = self.hash(item, i)
            self.C[i][j] += count

    def estimate(self, item):
        return min(self.C[i][self.hash(item, i)] for i in range(self.d))

    def __add__(self, other):
        if self.w != other.w or self.d != other.d:
            raise ValueError("CMS must have same dimensions")
        result = CountMinSketch(w=self.w, d=self.d, hash_func=self.hash_func)
        for i in range(self.d):
            for j in range(self.w):
                result.C[i][j] = self.C[i][j] + other.C[i][j]
        return result

def stream_dates(size):
    base = datetime.datetime(2000, 1, 1)
    for i in range(size):
        # Создаём уникальные datetime с шагом 1 секунда
        yield base + datetime.timedelta(seconds=i)


STREAM_SIZES = {"norm": 250_000, "big": 1_150_000}
EPSILONS = [0.00001]


class TestCountMinSketch(unittest.TestCase):

    def run_stream_test(self, n_elements, epsilon):
        cms = CountMinSketch(epsilon=epsilon)
        stream = stream_dates(n_elements)
        counts = {}
        for item in stream:
            counts[item] = counts.get(item, 0) + 1
            cms.add(item)

        # Проверяем 100 элементов
        sample_items = list(counts.keys())[:100]
        errors = []
        for item in sample_items:
            est = cms.estimate(item)
            exact = counts[item]
            abs_err = abs(est - exact)
            errors.append((item, exact, est, abs_err))
        return errors

    def test_error_cms(self):
        for eps in EPSILONS:
            for size_name, n in STREAM_SIZES.items():
                errors = self.run_stream_test(n, eps)
                rel_err = sum(errors)/len(errors)
                print(f"{size_name} stream, epsilon={eps}, rel_error={rel_err:.2f}%")
                self.assertLess(rel_err, 100)

    def test_union_cms(self):
        cms1 = CountMinSketch(epsilon=0.05)
        cms2 = CountMinSketch(epsilon=0.05)
        stream1 = stream_dates(100_000)
        stream2 = stream_dates(150_000)
        counts = {}

        for item in stream1:
            counts[item] = counts.get(item, 0) + 1
            cms1.add(item)
        for item in stream2:
            counts[item] = counts.get(item, 0) + 1
            cms2.add(item)

        cms_union = cms1 + cms2
        sample_items = list(counts.keys())[:100]
        errors = []
        for item in sample_items:
            est = cms_union.estimate(item)
            exact = counts[item]
            abs_err = abs(est - exact)
            errors.append((item, exact, est, abs_err))
        rel_err = sum(errors[3])/len(errors)
        print(f"Union CMS rel_error={rel_err:.2f}%")
        self.assertLess(rel_err, 100)

# -----------------------------
# Сбор данных и графики
# -----------------------------
def collect_error_data(epsilons=EPSILONS, sizes=STREAM_SIZES):
    results = []
    for eps in epsilons:
        for size_name, n in sizes.items():
            cms = CountMinSketch(epsilon=eps)
            stream = stream_dates(n)
            counts = {}
            for item in stream:
                counts[item] = counts.get(item, 0) + 1
                cms.add(item)
            sample_items = list(counts.keys())[:100]
            rel_errors = []
            for item in sample_items:
                est = cms.estimate(item)
                exact = counts[item]
                rel_errors.append(abs(est - exact)/exact * 100)
            avg_rel_err = sum(rel_errors)/len(rel_errors)
            results.append((eps, size_name, avg_rel_err))
    return results

def plot_error(results):
    for eps in set(r[0] for r in results):
        xs = []
        ys = []
        for r in results:
            if r[0] == eps:
                xs.append(STREAM_SIZES[r[1]])
                ys.append(r[2])
        plt.plot(xs, ys, marker='o', label=f"epsilon={eps}")
    plt.xlabel("Stream size")
    plt.ylabel("Average relative error (%)")
    plt.title("Count-Min Sketch relative error vs stream size")
    plt.legend()
    plt.grid(True)
    plt.show()


if __name__ == "__main__":
    unittest.main()

results = collect_error_data()
# print_table(results)
plot_error(results)