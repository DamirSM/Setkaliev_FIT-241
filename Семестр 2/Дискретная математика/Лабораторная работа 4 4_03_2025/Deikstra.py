inf = float('inf')

# start = int(input())
# end = int(input())

start = 2
end = 4

matrix = [
    [inf, 3, 45, inf, 58],
    [3, inf, 7, 13, inf],
    [45, 7, inf, 58, 36],
    [inf, 13, 58, inf, 78],
    [58, inf, 36, 78, inf]
]

matrix_out = [
    [inf] * 5,
    [inf] * 5,
    [inf] * 5,
    [inf] * 5,
    [inf] * 5
]

selected_row = {1: inf, 2: inf, 3: inf, 4: inf, 5: inf}
selected_column = {1: inf, 2: inf, 3: inf, 4: inf, 5: inf}

selected_row[start] = 0

while selected_row[end] == inf:
    for i in range(1, 6):
        if selected_row[i] == inf: 
            if matrix[start - 1][i-1] < selected_column[i]:
                matrix_out[start - 1][i-1] = matrix[start - 1][i-1] + selected_row[start]
                selected_column[i] = matrix_out[start - 1][i-1]
                print (selected_column)
            else:
                matrix_out[start - 1][i-1] = selected_column[i]
    next = matrix_out[start - 1].index(min(matrix_out[start - 1])) + 1
    selected_row[next] = min(matrix_out[start - 1])
    start = next
    print (matrix_out)

print(selected_row[end])