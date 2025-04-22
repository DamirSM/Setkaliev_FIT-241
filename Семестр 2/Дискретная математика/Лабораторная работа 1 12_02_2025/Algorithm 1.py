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

vertices = []
for i in range(n):
    vertices.append(i+1)

components = []

print(vertices)

while len(vertices) != 0:
    new_list = []
    new_list.append(vertices.pop(0))
    number = 0
    while number != len(new_list):
        for i in range(n):
            if (matrix[new_list[number]-1][i] != 0) and (i+1 not in new_list):
                new_list.append(vertices.pop(vertices.index(i+1)))
        number += 1
    components.append(new_list)

print(components)
print("Количество компонент связности:", len(components))