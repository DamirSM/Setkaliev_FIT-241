inf = float('inf')
matrix = [
    [0, 0, 2, 0, 0],
    [0, 0, 0, 7, 3],
    [2, 0, 0, 15, 0],
    [0, 7, 15, 0, 8],
    [0, 3, 0, 8, 0]
]

length = len(matrix)
visited = [0]
edges = []
for i in range(length):
    for j in range(i, length):
        if matrix[i][j] != 0:
            edges.append((i, j, matrix[i][j]))
print(edges)
m = len(edges)
for i in range(m):
    for j in range(0, m - i - 1):
        if edges[j][2] > edges[j + 1][2]:
            temp = edges[j]
            edges[j] = edges[j + 1]
            edges[j + 1] = temp
print(edges)
edge_sum = 0

edges_res = []
for n in range(m):
    for i in range(m):
        u = edges[i][0]
        v = edges[i][1]
        weight = edges[i][2]
        if (u in visited and v not in visited) or (v in visited and u not in visited):
            edges_res.append(edges[i])
            edge_sum += weight
            if u in visited:
                visited.append(v)
            else:
                visited.append(u)
            break
print(edges_res)
print(visited)
print(edge_sum)