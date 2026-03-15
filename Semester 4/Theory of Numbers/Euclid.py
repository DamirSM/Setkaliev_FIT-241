import numpy as np

a = 35375
b = 1880

def to_bin(n):
    return bin(n)[2:]

# Шаг 1
g = 1
u = a
v = b

steps = []

# Шаг 2: делим на 2, пока оба четные
while u % 2 == 0 and v % 2 == 0:
    u //= 2
    v //= 2
    g *= 2
    steps.append(("2", to_bin(u), to_bin(v), g))

steps.append(("3", to_bin(u), to_bin(v), g))

# Основной цикл (шаг 4)
while u != 0:
    # Шаг 4.1: пока u четное
    while u % 2 == 0:
        u //= 2
        steps.append(("4.1", to_bin(u), "", ""))
    # Шаг 4.2: пока v четное
    while v % 2 == 0:
        v //= 2
        steps.append(("4.2", "", to_bin(v), ""))
    # Шаг 4.3: сравнение и вычитание
    if u >= v:
        u -= v
        steps.append(("4.3", to_bin(u), "u := u - v", ""))
    else:
        v -= u
        steps.append(("4.3", "v := v - u", to_bin(v), ""))

# Результат
d = g * v
steps.append(("5", to_bin(u), to_bin(v), f"d = {g} * {to_bin(v)}"))
# Вывод шагов
for s,u_bin,v_bin,g_val in steps:
    print(f"{s} & {u_bin} & {v_bin} & {g_val} \\\\\n\\hline")

print(f"\nНОД({a},{b}) = {d}")