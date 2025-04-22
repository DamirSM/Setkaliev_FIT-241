inf = float("inf")

matrix = [
    [inf, 7, 16, 21, 2, 17],
    [13, inf, 21, 15, 43, 23],
    [25, 3, inf, 31, 17, 9],
    [13, 10, 27, inf, 33, 12],
    [9, 2, 19, 14, inf, 51],
    [42, 17, 5, 9, 23, inf],

]

columns = []
rows = []
for i in range(len(matrix)):
    rows.append(i+1)
    columns.append(i+1)


edges = []

sum = 0
for n in range(len(matrix) - 1):
    length = len(matrix)

    for i in range(length):
        min_i = min(matrix[i])
        sum += min_i
        for j in range(length):
            matrix[i][j] -= min_i

    min_j_list = [inf] * length
    for i in range(length):
        for j in range(length):
            if matrix[j][i] < min_j_list[i]: 
                min_j_list[i] = matrix[j][i]
        sum += min_j_list[i]

    for i in range(length):
        for j in range(length):
            matrix[j][i] -= min_j_list[i]

    min_i_list = [inf] * length
    for i in range(length):
        if matrix[i].count(0) > 1:
            min_i_list[i] = 0
        else:
            min_i_list[i] = min(filter(lambda x: x != 0, matrix[i]))

    min_j_list = [inf] * length
    for i in range(length):
        zero_count = 0
        for j in range(length):
            if matrix[j][i] == 0:
                zero_count += 1
            elif matrix[j][i] < min_j_list[i]:
                min_j_list[i] = matrix[j][i]
            if zero_count > 1:
                min_j_list[i] = 0
                break

    max_val = 0
    max_i, max_j = 0, 0
    for i in range(length):
        for j in range(length):
            if matrix[i][j] == 0:
                zero_coef = min_i_list[i] + min_j_list[j]
                if zero_coef > max_val:
                    max_val = zero_coef
                    max_i, max_j = i, j

    matrix[max_j][max_i] = inf

    edges.append((rows[max_i], columns[max_j]))
    rows.pop(max_i)
    columns.pop(max_j)

    matrix = [
        [val for j, val in enumerate(row) if j != max_j]
        for i, row in enumerate(matrix) if i != max_i
    ]

edges.append((rows[0], columns[0]))
print("Оптимальный путь:", edges)
print("Минимальная стоимость:", sum)