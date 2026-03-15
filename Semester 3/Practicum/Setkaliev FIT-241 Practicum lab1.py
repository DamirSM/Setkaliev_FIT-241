import numpy as np
my_array = np.arange(10, 70, 2)
print(f"{my_array}\n")

A = my_array.reshape(6,5)
A = A.T
print(f"Transposed A:\n{A}")

A = np.dot(A, 2.5)
A = np.subtract(A, 5)
print(A)

B = np.random.randint(0,10,18).reshape(6,3)
print(f"\nB:\n{B}")

a = np.sum(A, 1)
b = np.sum(B, 0)
print(f"\nSize of a: {np.size(a)},\nSize of b: {np.size(b)}\n")

print(np.matmul(A, B))

A = np.delete(A, 2, 1)
print(A)
B = np.concatenate((B, np.random.randint(10,20,18).reshape(6,3)), 1)
print(B)

det_A = np.linalg.det(A)
det_B = np.linalg.det(B)
print(f"\nDeterminant of A: {det_A}\nDeterminant of B: {det_B}\n")
if det_A != 0:
    inv_A = np.linalg.inv(A)
    print(f"Inverted A:\n{inv_A}")
if det_B != 0:
    inv_B = np.linalg.inv(B)
    print(f"Inverted B:\n{inv_B}")


A_power = np.linalg.matrix_power(A, 6)
B_power = np.linalg.matrix_power(B, 16)
print(f"\nA power 6:\n{A_power}\n\nB power 16:\n{B_power}\n")

X = np.array([2.3, 0, -3.4, -12, 2.6, 8.4, -9, 3, 1.3, 4.5, -17, 2, 1.8, 0, 15, 16]).reshape(4, 4)
b = np.array([-14, 0.4, -3.6, 17.4]).reshape(4,1)
print(X)
print(b)
result = np.linalg.solve(X, b)
print(f"\nResult of SLE:\n{result}")