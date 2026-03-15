epsilon = 1E-14
import matplotlib.pyplot as plt
import numpy as np
import scipy
import math
from scipy.interpolate import make_interp_spline

distr = np.array([348,348,344,333,334,350,352,339,347,333,340,349,336,337,355,348,335,338,347,
                  333,347,347,332,336,338,355,338,339,346,333,335,356,341,355,335,343,339,353,
                  345,331,335,353,346,354,344,336,332,339,335])
distr=sorted(distr)
print(distr)

n = len(distr)

values, counts = np.unique(distr, return_counts=True)

print(len(distr))
print(len(values))
print(counts)
count = 0
count_distr = []
for i in range(len(values)):
     print(values[i], count, count/49)
     count += counts[i]
     count_distr.append(count/49)
# plt.bar(values, count_distr)
# plt.xticks(values)
# plt.title("Эмпирическая функция распределения", fontsize=25)
# plt.xlabel("Values", fontsize=15)
# plt.ylabel("Counts", fontsize=15)
# plt.show()


values, counts = np.unique(distr, return_counts=True)
cum_freq = np.cumsum(counts) / n  # накопленная относительная частота

plt.figure(figsize=(10,6))

plt.hlines(0, values[0]-1, values[0], colors='blue', linewidth=2)
for i in range(len(values)):
    x_start = values[i]
    if i < len(values) - 1:
        x_end = values[i+1]
    else:
        x_end = values[i] + 1
    plt.hlines(cum_freq[i], x_start, x_end, colors='blue', linewidth=2)

plt.xticks(values, rotation=45)
plt.yticks(np.arange(0, 1.05, 0.05))
plt.xlabel("X_i")
plt.ylabel("F(x)")
plt.title("Эмпирическая функция распределния")
plt.grid(True)
plt.show()


X_ = sum(distr)/n
print(X_)

distr_subtr = (distr - X_)**2
print(distr_subtr)
S = 1/(n-1) * sum(distr_subtr)
print(S)

# mode = scipy.stats.mode(distr)
# print(mode.mode)

median = np.median(distr)
print(median)

skewness = scipy.stats.skew(distr)
print(skewness)

kurtosis = scipy.stats.kurtosis(distr)
print(kurtosis)

x_min = min(distr)
x_max = max(distr)
distr_range = x_max - x_min

interval_count = math.ceil(1 + 3.322 * math.log(n, 10))
print(interval_count)

interval_length = math.ceil(distr_range / interval_count)
print("Длина интервала:", interval_length)

x_0 = x_min - interval_length/2
print(x_0)

intervals = []
n_i = []
n_i_n = []
n_i_nh = []
for i in range(interval_count):
     x_i = x_0 + interval_length
     intervals.append((x_0, x_i))
     print(intervals)

     count = np.sum((distr >= x_0) & (distr < x_i))
     n_i.append(count)
     print(n_i)

     n_i_n.append(count/n)
     print(n_i_n)

     n_i_nh.append(count/(n*interval_length))
     print(n_i_nh)

     x_0 = x_i

centers = np.array([(interval[0]+interval[1])/2 for interval in intervals])
frequencies = np.array(n_i_nh)

x_smooth = np.linspace(centers.min(), centers.max(), 300)
spl = make_interp_spline(centers, frequencies, k=3)
y_smooth = spl(x_smooth)

x_ticks = np.arange(x_min - interval_length/2, x_max + interval_length, interval_length)

plt.figure(figsize=(10,6))
plt.bar(centers, n_i_nh, width=interval_length, alpha=0.5, edgecolor='black', label='Гистограмма')
plt.plot(x_smooth, y_smooth, color='red', linewidth=2, label='Полигон частот')
plt.xticks(x_ticks)
plt.xlabel("X_i")
plt.ylabel("n_i/(n*h)")
plt.title("Гистограмма и полигон частот")
plt.legend()
plt.grid(True)
plt.show()