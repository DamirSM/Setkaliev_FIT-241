inf = float('inf')

def Components(matrix):
    n = len(matrix)

    vertices = [i for i in range(n)]
    new_list = vertices

    for i in range(n):
        for j in range(n):
            if (matrix[i][j] != 0) and (new_list[j]>new_list[i]):
                new_list[j] = new_list[i]

    components = set(new_list)
    return len(components)

def Tree(matrix):
    length = len(matrix)
    visited = [0]
    edges = []
    edge_sum = 0
    for _ in range(length - 1):
        min_e = inf
        for n in range(length):
            if (n in visited):
                for i in range(length):
                    edge = matrix[n][i]
                    if i not in visited and edge < min_e and edge != 0:
                        min_e = edge
                        min_i = i
                        parent = n
        if min_e != inf:
            visited.append(min_i)
            edges.append((parent, min_i, min_e))
            edge_sum += min_e
    return(edges)

matrix = [
    [0, 0, 0, 0, 0, 1, 0, 1],
    [0, 0, 0, 1, 1, 0, 0, 0],
    [0, 0, 0, 0, 0, 1, 1, 0],
    [0, 1, 0, 0, 1, 0, 0, 0],
    [0, 1, 0, 1, 0, 0, 0, 0],
    [1, 0, 1, 0, 0, 0, 1, 0],
    [0, 0, 1, 0, 0, 1, 0, 0],
    [1, 0, 0, 0, 0, 0, 0, 0]
]

edges = Tree(matrix)
bridges = []

for i in range(len(edges)):
    temp = [row[:] for row in matrix]
    temp[edges[i][0]][edges[i][1]] = 0
    if Components(temp) > Components(matrix):
        print(Components(temp))
        bridges.append(edges[i])

if len(bridges) == 0:
    print("Мостов нет")
else:
    print("Количество мостов: ", len(bridges))
    print("Рёбра, являющиеся мостами: ", *bridges)