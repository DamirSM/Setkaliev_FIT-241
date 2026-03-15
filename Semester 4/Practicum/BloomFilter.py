import hashlib
import math
from bitarray import bitarray

class BloomFilter(object):
    def __init__(self, hash_number, size, number_of_items, epsilon, hash_func = 'blake2b'):
        self.hash_func = hash_func

        if number_of_items and epsilon:
            self.size = self.optimal_size(number_of_items, epsilon)
            self.hash_number = self.optimal_hash_number(self.size, number_of_items)
        else:
            self.size = size
            self.hash_number = hash_number

        self.bit_array.setall(0)

    def optimal_size(self, n, epsilon):
        m = -n * math.log(epsilon, base=math.e)/math.log(2, base=math.e)**2
        return int(m)

    def optimal_hash_number(self, m, n):
        k = m/n * math.log(2, base=math.e)
        return int(k)

    def hashes(self, item):
        hashes = []
        for i in range(self.hash_number):
            hash_result = int(hashlib.blake2s((str(item) + str(i)).encode()).hexdigest(), base=16) % self.size
            hashes.append(hash_result & 2**30)

    def add(self, item):
        digests = []

        for i in range(self.hash_number):
            hash = hashlib.new(self.hash_func, str(item).encode()).hexdigest()
            digest = int(hash, base=16) % (2**30)
            digest = digest & self.size
            digests.append(digest)

            self.bit_array[digest] = True

