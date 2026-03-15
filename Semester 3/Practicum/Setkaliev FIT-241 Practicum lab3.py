epsilon = 1E-14
import numpy as np
from scipy.linalg import lu
from scipy import stats as st

def print_no_brackets(X):

    def format_number(x):
        if isinstance(x, int):
            return str(x)
        else:
            if (np.abs(x - int(x)) <= epsilon):
                return str(int(round(x)))
            else:
                return str(x)

    if X.ndim == 1:
        print(" ".join(format_number(x) for x in X))
    else:
        for row in X:
            print(" ".join(format_number(x) for x in row))

print("1.")
X = np.array([2.3, 0, -3.4, -12, 2.6, 8.4, -9, 3, 1.3, 4.5, -17, 2, 1.8, 0, 15, 16]).reshape(4, 4)
b = np.array([-14, 0.4, -3.6, 17.4]).reshape(4,1)
A = np.linalg.solve(X, b)
print_no_brackets(X)
print()
print_no_brackets(b)
print()
print_no_brackets(A)


print("\n2.")
P, L, U = lu(A)
print("Permutation matrice P:")
print_no_brackets(P)
print("\nLower triangular matrice L:")
print_no_brackets(L)
print("\nUpper triangular matrice U:")
print_no_brackets(U)


print("\n3.")
inv_P = np.linalg.inv(P)
det_inv_P = np.linalg.det(inv_P)
print("Determinant of inverted P:")
print(det_inv_P)

diag_prod = np.diagonal(L) * np.diagonal(U)
print_no_brackets(diag_prod)

det = det_inv_P * diag_prod
print("\nDeterminant of A")
print_no_brackets(det)


print("\n4.")
size = 100
low = 0
high = 100
centre = (low + high)/2
sigma = (high - low)/6

uni_distr = np.random.randint(low, high+1, size)
print_no_brackets(uni_distr)

normal_distr = np.random.normal(loc=centre, scale=sigma, size=size)
clipped_normal_distr = np.clip(normal_distr, low, high).astype("int")

print_no_brackets(clipped_normal_distr)


print("\n5.")

def stats_print(distr):
    print("Mean:", np.mean(distr))
    print("Mode:", st.mode(distr).mode)
    print("Median:", np.median(distr))
    print("Minimum:", np.min(distr))
    print("Maximum:", np.max(distr))
    print("Standard deviation:", np.std(distr, ddof=1))

print("Statistics of uniform distribution")
stats_print(uni_distr)

print("\nStatistics of normal distribution")
stats_print(clipped_normal_distr)


print("\n6.")

observed, bin_edges = np.histogram(clipped_normal_distr, bins=10, range=(0, 100))

expected = np.array([10]*10)

chi2_stat, p_value = st.chisquare(f_obs=observed, f_exp=expected)
inv_p_value = 1 - p_value
print("p-value:", inv_p_value)
