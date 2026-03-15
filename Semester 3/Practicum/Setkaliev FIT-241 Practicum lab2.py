epsilon = 1E-14
import numpy as np
import sympy as sp
from scipy.interpolate import UnivariateSpline
from scipy import integrate
from scipy import optimize

def print_no_brackets(X):

    def format_number(x):
        if isinstance(x, int):
            return str(x)
        else:
            if (np.abs(x - int(x)) <= epsilon):
                return str(int(round(x)))
            else:
                return str(round(x,3))

    if X.ndim == 1:
        print(" ".join(format_number(x) for x in X))
    else:
        for row in X:
            print(" ".join(format_number(x) for x in row))

def f(x):
    return 2 / (np.sin(x) + 4)

print("1.")

x0 = 2

x = np.linspace(x0 - 1, x0 + 1, 70)
y = f(x)

spl = UnivariateSpline(x, y)

df1 = spl.derivative(n=1)(x0)
print(f"First derivative in x0 = {x0}: {df1}")

df2 = spl.derivative(n=2)(x0)
print(f"Second derivative in x0 = {x0}: {df2}")


print("\n2.")

x = sp.Symbol("x")
y = 2 / (sp.sin(x) + 4)

dy1 = sp.diff(y, x, 1)
dy2 = sp.diff(y, x, 2)

print("y =", y)
print("First derivative =", dy1)
print("Second derivative =", dy2)


print("\n3.")

a, b = 3, 6

result, err = integrate.quad(f, a, b)

print(f"Integral solved by the left rectangle method from {a} to {b}: {result}")


print("\n4.")

x = sp.Symbol("x")
y = 2 / (sp.sin(x) + 4)

iy = sp.integrate(y, x)

print("Integral:", iy)


print("\n5.")

fun = lambda x: (x[0] - 4)**2 + (x[1] - 2)**2

cons = ({'type': 'ineq', 'fun': lambda x: 4*x[0] + 2*x[1] - 11},
        {'type': 'ineq', 'fun': lambda x: -2*x[0] + 7})

bnds = ((0, None), (0, None))

x0 = [4, 0]

res = optimize.minimize(fun, x0, bounds=bnds, constraints=cons)

print("Optimal solution:")
print_no_brackets(res.x)
print("Minimum value of the objective function:", round(res.fun,2))
print("Success of the solution:", res.success)
