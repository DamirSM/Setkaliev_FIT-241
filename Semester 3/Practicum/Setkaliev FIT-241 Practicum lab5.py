import pandas as pd

print('1.')
df = pd.DataFrame({
    'Name': ['Strom, Mrs. Wilhelm (Elna Matilda Persson)', 'Navratil, Mr. Michel ("Louis MHoffman")', 'Minahan, Miss. Daisy E'],
    'Age': [29, 36.5, 33],
    'Sex': ['Female', 'Male', 'Female'],
})
print(df)

print('\n2.')
titanic_df1 = pd.read_csv('titanic_csv.csv', sep=';')
titanic_df1.columns = titanic_df1.columns.str.title()
titanic_df1 = titanic_df1.rename(columns={"Passengerid": "PassengerID"})
print(titanic_df1)

print('\n3.')
titanic_df2 = pd.read_csv('https://gist.githubusercontent.com/zaryanezrya/8b4ef51c707cb16d5e88a44dc00a1bb2/raw/41230f49c6268e072dbf102672f670be256922ab/gistfile1.txt')
titanic_df2.columns = titanic_df2.columns.str.title()
titanic_df2 = titanic_df2.rename(columns={"Passengerid": "PassengerID"})
print(titanic_df2)

print('\n4.')
titanic_df = pd.concat([titanic_df1, titanic_df2], ignore_index=True)
print(titanic_df[titanic_df.duplicated()])
titanic_df = titanic_df.drop_duplicates()
print(titanic_df)

print('\n5.')
titanic_df = titanic_df.set_index("PassengerID").sort_values(by="PassengerID")
print(titanic_df)

print('\n6.')
print(titanic_df.info(),'\n')
print(titanic_df.describe())

print('\n7.')
zero_row = titanic_df.iloc[0]
row_ind2 = titanic_df.loc[2]
print("Before:")
print(zero_row)
print(row_ind2)
titanic_df.iloc[0] = row_ind2
titanic_df.loc[2] = zero_row
print("After:")
print(titanic_df.iloc[0])
print(titanic_df.loc[2])

print('\n8.')
mapping = {"female": "f",
           "male": "m"}
titanic_df["Sex"] = titanic_df["Sex"].map(mapping)
print(titanic_df['Sex'])

print('\n9.')
ticket_counts = titanic_df.groupby("Ticket")["Name"].count()
print(ticket_counts[ticket_counts >= 6])
tickets_6 = ticket_counts[ticket_counts >= 6]
# print(tickets_6.index)
# print(titanic_df["Ticket"].isin(tickets_6.index))
print(titanic_df[titanic_df["Ticket"].isin(tickets_6.index)])

print('\n10.')
names1 = df["Name"]
print(names1)
cabins1 = titanic_df[titanic_df["Name"].isin(names1)]["Cabin"]
print(cabins1)
companions = titanic_df[titanic_df["Cabin"].isin(cabins1)]
print(companions)

print('\n11.')
titanic_df["BirthYear"] = (1912 - titanic_df["Age"]).dropna().astype(int)
print(titanic_df)

print('\n12.')
titanic_df["Companions"] = titanic_df.groupby("Cabin")["Name"].transform("count") - 1
print(titanic_df)

print('\n13.')
zero_row = titanic_df.iloc[0]
row_ind1 = titanic_df.loc[1]
titanic_df.iloc[0] = row_ind1
titanic_df.loc[1] = zero_row
print(titanic_df)

print('\n14.')
titanic_df = titanic_df.fillna(0)
titanic_df.to_csv("titanic_csv_processed.csv", index=True)

print('\n15.')
top_10_fare = titanic_df.groupby(["PassengerID","Name"])["Fare"].mean().sort_values(ascending=False).head(10)
print(top_10_fare)
top_10_fare = titanic_df.sort_values(by="Fare", ascending=False).head(10)
print(top_10_fare)

print('\n16.')
sex_survivours = titanic_df.groupby(["Sex", "Survived"])["Name"].count()
print(sex_survivours)

print('\n17.')
class_survivours = titanic_df.groupby(["Pclass", "Survived"])["Name"].count()
print(class_survivours)