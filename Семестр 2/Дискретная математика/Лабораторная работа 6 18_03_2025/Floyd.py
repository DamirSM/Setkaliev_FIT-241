inf = float('inf')

matrix = [
    [0, 10, 18, 8, inf, inf],
    [10, 0, 16, 9, 21, inf],
    [inf, 16, 0, inf, inf, 15],
    [7, 9, inf, 0, inf, 12],
    [inf, inf, inf, inf, 0, 23],
    [inf, inf, 15, inf, 23, 0]
]

matrix_res = matrix

for k in range(6):
    matrix_base = matrix_res
    for i in range(6):
        for j in range(6):
            matrix_res[i][j] = min(matrix_base[i][k] + matrix_base[k][j], matrix_base[i][j])

print(matrix_res)