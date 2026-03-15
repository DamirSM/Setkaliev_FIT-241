import numpy as np


def nf(x):
    return 0.1 * x ** 4 + 0.1 * x ** 2 + np.sin(x ** 3 + 3)

print(f"{nf(1.134):.3f}")

x_1, x_2, f_1, f_2 = None, None, None, None
a = 0.1
b = 1.7
alpha = 1 - (np.sqrt(5) - 1) / 2
epsilon = 0.1

print(alpha)

x_1 = a + (b - a) * alpha
x_2 = b - (b - a) * alpha
#
# print(f"{x_1:.3f}, {x_2:.3f}")

f_1 = nf(x_1)
f_2 = nf(x_2)

# print(f_1, f_2)
# print(f"{f_1:.3f}, {f_2:.3f}")

k=0

print(f"{k} & {a:.3f} & {b:.3f} & {(b-a):.3f} & {x_1:.3f} & {x_2:.3f} & {f_1:.3f} & {f_2:.3f} \\\\")

while (b - a) > 2*epsilon:

    if (f_1 > f_2):

        a = x_1
        f_1 = f_2
        x_1 = x_2
        x_2 = b - (b - a) * alpha
        f_2 = nf(x_2)

    else:

        b = x_2
        f_2 = f_1
        x_2 = x_1
        x_1 = a + (b - a) * alpha
        f_1 = nf(x_1)
    k+=1
    print(f"{k} & {a:.3f} & {b:.3f} & {(b - a):.3f} & {x_1:.3f} & {x_2:.3f} & {f_1:.3f} & {f_2:.3f} \\\\")

x = (a + b)/2
f = nf(x)
print(f"{x:.3f}")
print(f"{f:.3f}")