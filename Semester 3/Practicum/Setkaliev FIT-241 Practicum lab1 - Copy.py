import numpy as np

def print_no_brackets(X):

    def format_number(x):
        if isinstance(x, int):
            return str(int(x))
        else:
            if np.isclose(x, int(x)):
                return str(int(round(x)))
            else:
                return str(x)

    if X.ndim == 1:
        print(" ".join(format_number(x) for x in X))
    else:
        for row in X:
            print(" ".join(format_number(x) for x in row))

print("1.")
my_array = np.arange(10, 70, 2)
print_no_brackets(my_array)

print("\n2.")
A = my_array.reshape(6,5)
A = A.T
print(f"Transposed A:")
print_no_brackets(A)

print("\n3.")
A = np.dot(A, 2.5)
A = np.subtract(A, 5)
print_no_brackets(A)

print("\n4.")
B = np.random.randint(0,10,18).reshape(6,3)
print("B:")
print_no_brackets(B)

print("\n5.")
a = np.sum(A, 1)
b = np.sum(B, 0)
print(f"Size of a: {np.size(a)},\nSize of b: {np.size(b)}\n")

print("\n6.")
print_no_brackets(np.matmul(A, B))

print("\n7.")
A = np.delete(A, 2, 1)
print_no_brackets(A)
print()
B = np.concatenate((B, np.random.randint(10,20,18).reshape(6,3)), 1)
print_no_brackets(B)

print("\n8.")
det_A = np.linalg.det(A)
det_B = np.linalg.det(B)
print(f"Determinant of A: {det_A}\nDeterminant of B: {det_B}\n")
if det_A != 0:
    inv_A = np.linalg.inv(A)
    print("Inverted A:\n")
    print_no_brackets(inv_A)
if det_B != 0:
    inv_B = np.linalg.inv(B)
    print("Inverted B:\n")
    print_no_brackets(inv_B)

print("\n9.")
A_power = np.linalg.matrix_power(A, 6)
B_power = np.linalg.matrix_power(B, 14)
print("A power 6:")
print_no_brackets(A_power)
print("\nB power 14:")
print_no_brackets(B_power)


print("\n10.")
X = np.array([2.3, 0, -3.4, -12, 2.6, 8.4, -9, 3, 1.3, 4.5, -17, 2, 1.8, 0, 15, 16]).reshape(4, 4)
b = np.array([-14, 0.4, -3.6, 17.4]).reshape(4,1)
print_no_brackets(X)
print()
print_no_brackets(b)
result = np.linalg.solve(X, b)
print("\nResult of SLE:")
print_no_brackets(result)