epsilon = 0.01
import numpy as np
import sympy as sp
from scipy.interpolate import UnivariateSpline

def nf(x):
    return 0.1*x**4 + 0.1*x**2 + np.sin(x**3 + 3)

def sf(x):
    return 0.1*x**4 + 0.1*x**2 + sp.sin(x**3 + 3)

a_k = -1.5
b_k = 2

k = 0
x_k = (a_k + b_k)/2

x = np.linspace(x_k - 0.001, x_k + 0.001, 100)
y = nf(x)

x_symb = sp.Symbol("x")
y_symb = sf(x_symb)
print(y_symb)

dy1 = sp.diff(y_symb, x_symb, 1)

print("First derivative =", dy1)

spl = UnivariateSpline(x, y)

df1 = spl.derivative(n=1)
df1 = df1(x_k)
print(f"First derivative in x0 = {x_k}: {df1}")

while (b_k-a_k) > 2*epsilon:
    print(f"{k} & {a_k:.3f} & {b_k:.3f} & {b_k-a_k:.3f} & {x_k:.3f} & {df1:.3f} & {nf(x_k):.3f}\\\\")

    if df1 > 0:
        b_k = x_k
    else:
        a_k = x_k
    x_k = (a_k + b_k)/2

    x = np.linspace(x_k - 0.001, x_k + 0.001, 100)
    y = nf(x)
    spl = UnivariateSpline(x, y)

    df1 = spl.derivative(n=1)
    df1 = df1(x_k)

    k += 1
print(f"{k} & {a_k:.3f} & {b_k:.3f} & {b_k-a_k:.3f} & {x_k:.3f} & {df1:.3f} & {nf(x_k):.3f}\\\\")
print(f"{nf(x_k):.4f}")