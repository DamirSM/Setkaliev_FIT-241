import numpy as np

def nf(x):
    return (x+5)**4

x_1, x_2, f_1, f_2 = None, None, None, None
a = -6
b = 2
epsilon = 0.1

k = 0
while np.abs(a - b) > 2*epsilon:
    x = (a + b)/2

    x_1 = x - epsilon/2
    x_2 = x + epsilon/2

    f_1 = nf(x_1)
    f_2 = nf(x_2)

    print(f"{k} & {a:.3f} & {b:.3f} & {np.abs(a - b):.3f} & {x_1:.3f} & {x_2:.3f} & {f_1:.3f} & {f_2:.3f} \\\\")

    if (f_1>f_2):
        a = x_1
    else:
        b = x_2
    k += 1
print(f"{k} & {a:.3f} & {b:.3f} & {np.abs(a - b):.3f} &  &  &  & \\\\")
f = nf((a+b)/2)

print(f"{f:.3f}")