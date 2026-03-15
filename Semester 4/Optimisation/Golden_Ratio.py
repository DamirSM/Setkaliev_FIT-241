import numpy as np


def nf(x):
    return (x+5)**4


x_1, x_2, f_1, f_2 = None, None, None, None
a = -6
b = 2
tau = (np.sqrt(5) - 1) / 2
epsilon = 0.1

x_1 = a + (b - a) * tau
x_2 = b - (b - a) * tau

f_1 = nf(x_1)
f_2 = nf(x_2)

k=0
print(f"{k} & {a:.3f} & {b:.3f} & {np.abs(a - b):.3f} & {x_1:.3f} & {x_2:.3f} & {f_1:.3f} & {f_2:.3f} \\\\")
k=1
while (b - a) > epsilon:

    if (f_1 > f_2):

        b = x_1
        f_1 = f_2
        x_1 = x_2
        x_2 = b - (b - a) * tau
        f_2 = nf(x_2)

    else:

        a = x_2
        f_2 = f_1
        x_2 = x_1
        x_1 = a + (b - a) * tau
        f_1 = nf(x_1)
    print(f"{k} & {a:.3f} & {b:.3f} & {np.abs(a - b):.3f} & {x_1:.3f} & {x_2:.3f} & {f_1:.3f} & {f_2:.3f} \\\\")
    k+=1

f = min(f_1, f_2)

print(f"{f:.3f}")