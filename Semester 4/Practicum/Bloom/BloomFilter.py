import hashlib
import math
import random
import matplotlib.pyplot as plt
import unittest

#Вариант 7

class BloomFilter:
    def __init__(self, number_of_items, epsilon, hash_number = None, size = None, hash_func=hashlib.blake2b):
        if number_of_items is not None and epsilon is not None:
            m = self.optimal_size(number_of_items, epsilon)
            k = self.optimal_hash_number(m, number_of_items)
        else:
            m = size
            k = hash_number

        self.n = number_of_items
        self.epsilon = epsilon
        self.k = k
        self.m = m

        self.hash_func = hash_func
        self.array = [0] * m

    def optimal_size(self, n, epsilon):
        m = -n * math.log(epsilon)/math.log(2)**2
        return int(m)

    def optimal_hash_number(self, m, n):
        k = m/n * math.log(2)
        return max(1, int(k))

    def hashes(self, item):
        hashes = []

        for i in range(self.k):
            h = int(self.hash_func((str(item) + str(i)).encode()).hexdigest(), 16)
            h = (h % (2 ** 30)) % self.m
            hashes.append(h)
        return hashes

    def add(self, item):
        hashes = self.hashes(item)
        for h in hashes:
            self.array[h] = 1

    def check(self, item):
        hashes = self.hashes(item)
        for h in hashes:
            if self.array[h] == 0:
                return False
        return True

    def __add__(self, other):
        if self.m != other.m or self.k != other.k or self.hash_func != other.hash_func:
            raise ValueError("Filters must have same parameters and hash function")
        result = CountingBloomFilter(self.n, self.epsilon)

        for i in range(self.m):
            result.array[i] = self.array[i] | other.array[i]

        return result

    def __sub__(self, other):
        if self.m != other.m or self.k != other.k or self.hash_func != other.hash_func:
            raise ValueError("Filters must have same parameters and hash function")
        result = CountingBloomFilter(self.n, self.epsilon)

        for i in range(self.m):
            result.array[i] = self.array[i] & other.array[i]

        return result

class CountingBloomFilter(BloomFilter):

    def __init__(self, n, epsilon, hash_func=hashlib.blake2b):
        super().__init__(n, epsilon, hash_func=hash_func)

    def add(self, item):
        hashes = self.hashes(item)
        for h in hashes:
            self.array[h] += 1

    def remove(self, item):
        hashes = self.hashes(item)
        for h in hashes:
            if self.array[h] > 0:
                self.array[h] -= 1

    def __add__(self, other):
        if self.m != other.m or self.k != other.k or self.hash_func != other.hash_func:
            raise ValueError("Filters must have same parameters and hash function")
        result = CountingBloomFilter(self.n, self.epsilon)

        for i in range(self.m):
            result.array[i] = self.array[i] + other.array[i]

        return result

    def __sub__(self, other):
        if self.m != other.m or self.k != other.k or self.hash_func != other.hash_func:
            raise ValueError("Filters must have same parameters and hash function")
        result = CountingBloomFilter(self.n, self.epsilon)

        for i in range(self.m):
            result.array[i] = min(self.array[i], other.array[i])

        return result

def theoretical_error(k, n, m):
    return (1 - math.exp(-k * n / m)) ** k

def experiment(number_of_items, epsilon, fill_ratio):

    bf = CountingBloomFilter(n=number_of_items, epsilon=epsilon)

    n = int(number_of_items * fill_ratio)

    inserted = set(random.sample(range(n * 10), n))

    for x in inserted:
        bf.add(x)

    test_data = set(random.sample(range(n * 10, n * 20), n))

    false_positive = 0

    for x in test_data:
        if bf.check(x):
            false_positive += 1

    return false_positive / n

def variance(data):
    mean = sum(data) / len(data)
    return sum((x - mean) ** 2 for x in data) / (len(data) - 1)

params = [
    (25_000, 0.7),
    (130_000, 0.15),
    (750_000, 0.05)
]

ratios = [0.25, 0.5, 0.75, 0.95]

results_table = []

fig_num = 0
for n, eps in params:
    fig_num += 1

    bf = CountingBloomFilter(n=n, epsilon=eps)

    print("\nParameters:", n, eps)
    print("k =", bf.k, "m =", bf.m)

    fills = []
    exp_errors = []
    theory_errors = []

    for r in ratios:

        results = []

        for _ in range(3):
            results.append(experiment(n, eps, r))

        avg = sum(results) / len(results)
        var = variance(results)

        theory = theoretical_error(bf.k, int(n * r), bf.m)

        results_table.append((n, eps, r, avg, var, theory))

        fills.append(r)
        exp_errors.append(avg)
        theory_errors.append(theoretical_error(bf.k, int(n * r), bf.m))

        print(
            "fill =", r,
            "variance =", round(var, 6),
            "exp =", round(avg, 6),
            "theory =", round(theory, 6)
        )

    print(f"{'n':^20}{'epsilon':^20}{'fill':^20}{'exp_error':^20}{'variance':^20}{'theory':^20}")

    for row in results_table:
        print(f"{row[0]:^20}{row[1]:^20}{row[2]:^20}{row[3]:^20.6}{row[4]:^20.6}{row[5]:^20.6}")

    plt.plot(fills, exp_errors, label="Experimental error")
    plt.plot(fills, theory_errors, label="Theoretical error")

    plt.xlabel("Fill ratio")
    plt.ylabel("False positive probability")
    plt.title("Bloom Filter False Positives")
    plt.legend()
    plt.savefig(f"bloom_false_positives{fig_num}.png")
    plt.show()

with open("bloom_results.txt", "w", encoding="utf-8") as f:
    f.write(f"{'n':^20}{'epsilon':^20}{'fill':^20}{'exp_error':^20}{'variance':^20}{'theory':^20}\n")

    for row in results_table:
        f.write(f"{row[0]:^20}{row[1]:^20}{row[2]:^20}{row[3]:^20.6f}{row[4]:^20.6f}{row[5]:^20.6f}\n")

class TestBloom(unittest.TestCase):

    def test_insert_and_find(self):

        bf = CountingBloomFilter(n=1000, epsilon=0.1)
        bf.add("hello")

        self.assertTrue(bf.check("hello"))

    def test_remove(self):

        bf = CountingBloomFilter(n=1000, epsilon=0.1)

        bf.add("test")
        bf.remove("test")

        self.assertFalse(bf.check("test"))

    def test_union(self):

        bf1 = CountingBloomFilter(n=1000, epsilon=0.1)
        bf2 = CountingBloomFilter(n=1000, epsilon=0.1)

        bf1.add("a")
        bf2.add("b")

        bf3 = bf1 + bf2

        self.assertTrue(bf3.check("a"))
        self.assertTrue(bf3.check("b"))

    def test_intersection(self):

        bf1 = CountingBloomFilter(n=1000, epsilon=0.1)
        bf2 = CountingBloomFilter(n=1000, epsilon=0.1)

        bf1.add("a")
        bf2.add("a")

        bf3 = bf1 - bf2

        self.assertTrue(bf3.check("a"))

    def test_incorrect_parameters(self):

        bf1 = CountingBloomFilter(n=1000, epsilon=0.1)
        bf2 = CountingBloomFilter(n=2000, epsilon=0.2)

        with self.assertRaises(ValueError):
            bf1 + bf2

    def test_incorrect_hash_func(self):

        bf1 = CountingBloomFilter(n=1000, epsilon=0.1)
        bf2 = CountingBloomFilter(n=2000, epsilon=0.2, hash_func=hashlib.md5)

        with self.assertRaises(ValueError):
            bf1 + bf2

        with self.assertRaises(ValueError):
            bf1 - bf2


if __name__ == "__main__":
    unittest.main()