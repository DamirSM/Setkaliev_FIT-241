matrix = [
    [0,0,0,1,0,0,0,0,0,0],
    [0,0,0,0,0,1,0,0,1,0],
    [0,0,0,0,0,0,0,0,0,1],
    [1,0,0,0,0,0,0,0,0,0],
    [0,0,0,0,0,0,1,1,0,0],
    [0,1,0,0,0,0,0,0,1,0],
    [0,0,0,0,1,0,0,1,0,0],
    [0,0,0,0,1,0,1,0,0,0],
    [0,1,0,0,0,1,0,0,0,0],
    [0,0,1,0,0,0,0,0,0,0],
]

n = len(matrix)

count = 0

vertices = [i for i in range(n)]
new_list = vertices

print(vertices)

for i in range(n):
    for j in range(n):
        if (matrix[i][j] != 0) and (new_list[j]>new_list[i]):
            new_list[j] = new_list[i]
print(new_list)

components = set(new_list)
print("Количество компонент связности:", len(components))