inf = float('inf')
matrix = [
    [inf, 1, inf, inf, 3],
    [inf, inf, 8, 7, 1],
    [inf, inf, inf, 1, -5],
    [inf, inf, 2, inf, inf],
    [inf, inf, inf, 4, inf]
]

n = len(matrix)

start = int(input("Введите номер начальной вершины: ")) - 1

lambda_prev = [inf] * n
lambda_curr = [inf] * n
lambda_prev[start] = 0

for iteration in range(n - 1):
    for i in range(n):
        lambda_curr[i] = lambda_prev[i]
    for u in range(n):
        for v in range(n):
            w = matrix[u][v]
            if w != inf and lambda_prev[u] + w < lambda_curr[v]:
                lambda_curr[v] = lambda_prev[u] + w

    if lambda_curr == lambda_prev:
        break

    for i in range(n):
        lambda_prev[i] = lambda_curr[i]
    print(lambda_prev)

has_negative_cycle = False
for u in range(n):
    for v in range(n):
        w = matrix[u][v]
        if w != inf and lambda_prev[u] + w < lambda_prev[v]:
            has_negative_cycle = True
            break
    if has_negative_cycle:
        break

if has_negative_cycle:
    print("Обнаружен цикл отрицательного веса")
else:
    print("Кратчайшие расстояния от вершины", start + 1)
    for i in range(n):
        dist = lambda_prev[i]
        if dist == inf:
            print("До вершины ", i+1, " путь отсутствует")
        else:
            print("До вершины ", i+1, ": ", dist)