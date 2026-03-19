import hashlib
import math
import random
import datetime
import unittest
import matplotlib.pyplot as plt

#Вариант 3

def alpha_m(m):
    if m == 16:
        return 0.673
    elif m == 32:
        return 0.697
    elif m == 64:
        return 0.709
    elif m >= 128:
        return 0.7213 / (1 + 1.079 / m)

class HyperLogLog:
    def __init__(self, p=None, epsilon=None, q=32, hash_func=hashlib.sha256):
        if p is None and epsilon is not None:
            p = math.floor(2 * math.log2(1.04 / epsilon))

        self.p = max(4, p)
        self.m = 2 ** self.p
        self.q = q
        self.M = [0] * self.m
        self.hash_func = hash_func

    def hash(self, item):
        h = int(self.hash_func(str(item).encode()).hexdigest(), 16)
        return h

    def add(self, item):
        x = self.hash(item)
        hash_bits = 256
        j = x >> (hash_bits - self.p)
        w = x & ((1 << (hash_bits - self.p)) - 1)
        rank = self.first_one_bit(w, hash_bits - self.p)
        self.M[j] = max(self.M[j], rank)

    def first_one_bit(self, w, max_bits):
        i = 1
        while i <= max_bits and (w >> (max_bits - i)) & 1 == 0:
            i += 1
        return i

    def estimate(self):
        alpha = alpha_m(self.m)
        Z = 1 / sum([2 ** (-v) for v in self.M])
        E = alpha * self.m ** 2 * Z

        V = self.M.count(0)
        if V > 0 and E < (5/2) * self.m:
            E = self.m * math.log(self.m / V)

        if E > 2 ** self.q / 30:
            E = -2 ** 32 * math.log10(1 - E / 2 ** 32)

        return E

    def __add__(self, other):
        if self.m != other.m or self.hash_func != other.hash_func:
            raise ValueError("Filters must have same parameters and hash function")
        result = HyperLogLog(p=self.p, q=self.q, hash_func=self.hash_func)
        result.M = [max(self.M[i], other.M[i]) for i in range(self.m)]
        return result

def stream_dates():
    while True:
        yield datetime.datetime(
            random.randint(1900, 2030),
            random.randint(1, 12),
            random.randint(1, 28),
        )

STREAM_SIZES = {"small": 10_000, "norm": 250_000, "big": 1_150_000}
EPSILONS = [0.7, 0.15, 0.05]

class TestHyperLogLogStreams(unittest.TestCase):

    def run_stream_test(self, n_elements, epsilon):
        hll = HyperLogLog(epsilon=epsilon)
        stream = stream_dates()
        unique_items = set()
        for _ in range(n_elements):
            item = next(stream)
            unique_items.add(item)
            hll.add(item)

        est = hll.estimate()
        exact = len(unique_items)
        abs_error = abs(est - exact)
        rel_error = abs_error / exact * 100
        return est, exact, abs_error, rel_error

    def test_all_stream_sizes(self):
        for eps in EPSILONS:
            for size_name, n in STREAM_SIZES.items():
                est, exact, abs_err, rel_err = self.run_stream_test(n, eps)
                print(f"{size_name}: exact={exact}, est={int(est)}, "
                      f"abs_err={abs_err:.1f}, rel_err={rel_err:.2f}%")
                self.assertLess(rel_err, 50)
            print()

    def test_union_small_norm(self):
        hll1 = HyperLogLog(epsilon=0.05)
        hll2 = HyperLogLog(epsilon=0.05)
        stream = stream_dates()

        small_items = set()
        for _ in range(STREAM_SIZES["small"]):
            item = next(stream)
            small_items.add(item)
            hll1.add(item)

        norm_items = set()
        for _ in range(STREAM_SIZES["norm"]):
            item = next(stream)
            norm_items.add(item)
            hll2.add(item)

        hll_union = hll1 + hll2

        exact = len(small_items.union(norm_items))
        est = hll_union.estimate()
        abs_error = abs(est - exact)
        rel_error = abs_error / exact * 100

        print(f"union small+norm: est: {est:.0f}, exact: {exact}, "
              f"abs_error: {abs_error:.1f}, rel_error: {rel_error:.2f}%")

        self.assertLess(rel_error, 50)

def collect_error_data(epsilons=EPSILONS, sizes=STREAM_SIZES):
    results = []
    for eps in epsilons:
        for size_name, n in sizes.items():
            hll = HyperLogLog(epsilon=eps)
            stream = stream_dates()
            unique_items = set()
            for _ in range(n):
                item = next(stream)
                unique_items.add(item)
                hll.add(item)
            est = hll.estimate()
            exact = len(unique_items)
            abs_error = abs(est - exact)
            rel_error = abs_error / exact * 100
            results.append((eps, size_name, exact, int(est), rel_error))
    return results

def print_table(results):
    print(f"{'epsilon':^20}{'stream':^20}{'exact':^20}{'estimate':^20}{'rel_error %':^20}")

    for row in results:
        eps, stream, exact, est, rel_err = row
        print(f"{eps:^20.6f}{stream:^20}{exact:^20.6f}{est:^20.6f}{rel_err:^20.2f}")
    print()

    with open("hll_results.txt", "w", encoding="utf-8") as f:
        f.write(f"{'epsilon':^20}{'stream':^20}{'exact':^20}{'estimate':^20}{'rel_error %':^20}\n")
        for row in results:
            eps, stream, exact, est, rel_err = row
            f.write(f"{eps:^20.6f}{stream:^20}{exact:^20.6f}{est:^20.6f}{rel_err:^20.2f}\n")

def plot_error(results):
    for eps in set(r[0] for r in results):
        xs = []
        ys = []
        for r in results:
            if r[0] == eps:
                n = STREAM_SIZES[r[1]]
                xs.append(n)
                ys.append(r[4])
        plt.plot(xs, ys, marker='o', label=f"epsilon={eps}")
    plt.xlabel("Stream size")
    plt.ylabel("Relative error (%)")
    plt.title("HyperLogLog relative error vs stream size")
    plt.legend()
    plt.grid(True)
    plt.savefig(f"hll.png")
    plt.show()

if __name__ == "__main__":
    unittest.main()

results = collect_error_data()
print_table(results)
plot_error(results)