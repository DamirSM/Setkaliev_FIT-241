epsilon = 0.001
import numpy as np
import sympy as sp
from scipy.interpolate import UnivariateSpline

def nf(x):
    return 0.1*x**4 + 0.1*x**2 + np.sin(x**3 + 3)

def sf(x):
    return 0.1*x**4 + 0.1*x**2 + sp.sin(x**3 + 3)

k = 0
x_k = 1

x = np.linspace(x_k - 0.001, x_k + 0.001, 100)
y = nf(x)

x_symb = sp.Symbol("x")
y_symb = sf(x_symb)
print(y_symb)

dy1 = sp.diff(y_symb, x_symb, 1)
dy2 = sp.diff(y_symb, x_symb, 2)

print("First derivative =", dy1)
print("Second derivative =", dy2)

spl = UnivariateSpline(x, y)

df1 = spl.derivative(n=1)
df1 = df1(x_k)
print(f"First derivative in x0 = {x_k}: {df1}")

df2 = spl.derivative(n=2)
df2 = df2(x_k)
print(f"Second derivative in x0 = {x_k}: {df2}")

x_k1 = x_k - df1/df2

while np.abs(x_k1 - x_k) >= epsilon:
    print(f"{k} & {x_k:.3f} & {df1:.3f} & {df2:.3f} & {x_k1:.3f} & {nf(x_k):.3f}\\\\")
    x_k = x_k1

    x = np.linspace(x_k - 0.001, x_k + 0.001, 100)
    y = nf(x)
    spl = UnivariateSpline(x, y)

    df1 = spl.derivative(n=1)
    df1 = df1(x_k)

    df2 = spl.derivative(n=2)
    df2 = df2(x_k)

    x_k1 = x_k - df1/df2
    k += 1
print(f"{k} & {x_k:.3f} & {df1:.3f} & {df2:.3f} & {x_k1:.3f} & {nf(x_k):.3f}\\\\")
print(f"{nf(x_k):.4f}")