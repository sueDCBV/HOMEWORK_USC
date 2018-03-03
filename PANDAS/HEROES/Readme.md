

```python
 # Import Dependencies
import pandas as pd
import numpy as np
import glob, os
```


```python
 # Create a recursive call to read all json files
path = 'raw_data'
ls_all_json = glob.glob(os.path.join(path, "*.json"), recursive=True) 

df_heroes_temp = (pd.read_json(i) for i in ls_all_json)
df_heroes = pd.concat(df_heroes_temp, ignore_index=True)

#Cleaning data, drop empty rows
df_heroes = df_heroes.dropna(how="any")
df_heroes.head() 

```




<div>
<style>
    .dataframe thead tr:only-child th {
        text-align: right;
    }

    .dataframe thead th {
        text-align: left;
    }

    .dataframe tbody tr th {
        vertical-align: top;
    }
</style>
<table border="1" class="dataframe">
  <thead>
    <tr style="text-align: right;">
      <th></th>
      <th>Age</th>
      <th>Gender</th>
      <th>Item ID</th>
      <th>Item Name</th>
      <th>Price</th>
      <th>SN</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>0</th>
      <td>38</td>
      <td>Male</td>
      <td>165</td>
      <td>Bone Crushing Silver Skewer</td>
      <td>$3.37</td>
      <td>Aelalis34</td>
    </tr>
    <tr>
      <th>1</th>
      <td>21</td>
      <td>Male</td>
      <td>119</td>
      <td>Stormbringer, Dark Blade of Ending Misery</td>
      <td>$2.32</td>
      <td>Eolo46</td>
    </tr>
    <tr>
      <th>2</th>
      <td>34</td>
      <td>Male</td>
      <td>174</td>
      <td>Primitive Blade</td>
      <td>$2.46</td>
      <td>Assastnya25</td>
    </tr>
    <tr>
      <th>3</th>
      <td>21</td>
      <td>Male</td>
      <td>92</td>
      <td>Final Critic</td>
      <td>$1.36</td>
      <td>Pheusrical25</td>
    </tr>
    <tr>
      <th>4</th>
      <td>23</td>
      <td>Male</td>
      <td>63</td>
      <td>Stormfury Mace</td>
      <td>$1.27</td>
      <td>Aela59</td>
    </tr>
  </tbody>
</table>
</div>




```python
# Player Count
# Total Number of Players

#rename columns into a more understandable names
df_heroes = df_heroes.rename(columns={"SN":"Player"})

#Group by "Player" to obtain unique values
total_players = df_heroes.groupby(["Player"]).count()

#Count and store in dataframe
df_total_players = pd.DataFrame({"Total Players":[len(total_players)]}) 
df_total_players
```




<div>
<style>
    .dataframe thead tr:only-child th {
        text-align: right;
    }

    .dataframe thead th {
        text-align: left;
    }

    .dataframe tbody tr th {
        vertical-align: top;
    }
</style>
<table border="1" class="dataframe">
  <thead>
    <tr style="text-align: right;">
      <th></th>
      <th>Total Players</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>0</th>
      <td>612</td>
    </tr>
  </tbody>
</table>
</div>




```python
# Purchasing Analysis (Total)
# Number of Unique Items
# Average Purchase Price
# Total Number of Purchases
# Total Revenue

#Group by "Item ID" to obtain unique values
#**There are Item Names with the same Item ID, this could generate a confusion when showing results and making statistics
#**I counted Item ID instead of Item Name
total_items = df_heroes.groupby(["Item ID"]).count()
total_items=len(total_items)

#Get total average price
avg_price = df_heroes["Price"].mean()

#Get total purchases
total_purchases=len(df_heroes)

#Get total revenue
total_revenue = df_heroes["Price"].sum()

#Numbers into money format
pd.options.display.float_format = '${:,.2f}'.format
df_heroes_summary = pd.DataFrame({"Number of Unique Items":[total_items],"Average Purchase Price":[round(avg_price,2)]
                                 ,"Total Number of Purchases":[total_purchases],"Total Revenue":[round(total_revenue,2)]}) 
#Showing results
df_heroes_summary[["Number of Unique Items","Average Purchase Price","Total Number of Purchases","Total Revenue"]]

```




<div>
<style>
    .dataframe thead tr:only-child th {
        text-align: right;
    }

    .dataframe thead th {
        text-align: left;
    }

    .dataframe tbody tr th {
        vertical-align: top;
    }
</style>
<table border="1" class="dataframe">
  <thead>
    <tr style="text-align: right;">
      <th></th>
      <th>Number of Unique Items</th>
      <th>Average Purchase Price</th>
      <th>Total Number of Purchases</th>
      <th>Total Revenue</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>0</th>
      <td>184</td>
      <td>$2.93</td>
      <td>858</td>
      <td>$2,514.43</td>
    </tr>
  </tbody>
</table>
</div>




```python
# Gender Demographics
# Percentage and Count of Male Players
# Percentage and Count of Female Players
# Percentage and Count of Other / Non-Disclosed

#Aggregating data by Gender
df_heroes_gender_data=df_heroes.groupby(["Gender"])

#Storage results into data frame
df_heroes_gender_count = pd.DataFrame(df_heroes_gender_data["Item ID"].count())

#Rename columns for printing
df_heroes_gender_count =df_heroes_gender_count.rename(columns={"Item ID":"Total Count"})                                                                                                                                                                                                                                                                                                                                                                                                                                                        

#Create a new column with percentage of players
df_heroes_gender_count["Percentage of Players"] = (df_heroes_gender_count['Total Count']/total_purchases)*100

#Format percentage
pd.options.display.float_format = '%{:,.2f}'.format

#Sorting values descending
df_heroes_gender_count = df_heroes_gender_count.sort_values("Total Count",ascending=False)

#Show results
df_heroes_gender_count[["Percentage of Players","Total Count"]]

```




<div>
<style>
    .dataframe thead tr:only-child th {
        text-align: right;
    }

    .dataframe thead th {
        text-align: left;
    }

    .dataframe tbody tr th {
        vertical-align: top;
    }
</style>
<table border="1" class="dataframe">
  <thead>
    <tr style="text-align: right;">
      <th></th>
      <th>Percentage of Players</th>
      <th>Total Count</th>
    </tr>
    <tr>
      <th>Gender</th>
      <th></th>
      <th></th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>Male</th>
      <td>%81.24</td>
      <td>697</td>
    </tr>
    <tr>
      <th>Female</th>
      <td>%17.37</td>
      <td>149</td>
    </tr>
    <tr>
      <th>Other / Non-Disclosed</th>
      <td>%1.40</td>
      <td>12</td>
    </tr>
  </tbody>
</table>
</div>




```python
# Purchasing Analysis (Gender) 
# The below each broken by gender
# Purchase Count
# Average Purchase Price
# Total Purchase Value
# Normalized Totals

#With above dataframe (gender), aggregating by price
revenue_gender = df_heroes_gender_data["Price"].sum()

#Count values per gender
gender_counts = df_heroes["Gender"].value_counts()

#Data frame to storage values
df_purch_analysis = pd.DataFrame({"Purchase Count":gender_counts,
                                   "Total Purchase Value":revenue_gender})
#Applying format
pd.options.display.float_format = '${:,.2f}'.format

#Create a new column with average purchase price, sorting and showing results
df_purch_analysis["Average Purchase Price"] = df_purch_analysis["Total Purchase Value"] /  df_purch_analysis["Purchase Count"] 
df_purch_analysis = df_purch_analysis.sort_values("Total Purchase Value",ascending=False)
df_purch_analysis.head()
```




<div>
<style>
    .dataframe thead tr:only-child th {
        text-align: right;
    }

    .dataframe thead th {
        text-align: left;
    }

    .dataframe tbody tr th {
        vertical-align: top;
    }
</style>
<table border="1" class="dataframe">
  <thead>
    <tr style="text-align: right;">
      <th></th>
      <th>Purchase Count</th>
      <th>Total Purchase Value</th>
      <th>Average Purchase Price</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>Male</th>
      <td>697</td>
      <td>$2,052.28</td>
      <td>$2.94</td>
    </tr>
    <tr>
      <th>Female</th>
      <td>149</td>
      <td>$424.29</td>
      <td>$2.85</td>
    </tr>
    <tr>
      <th>Other / Non-Disclosed</th>
      <td>12</td>
      <td>$37.86</td>
      <td>$3.15</td>
    </tr>
  </tbody>
</table>
</div>




```python
# Age Demographics
# The below each broken into bins of 4 years (i.e. <10, 10-14, 15-19, etc.) 
# Purchase Count
# Average Purchase Price
# Total Purchase Value
# Normalized Totals


# Create the bins in which Data will be held
bins = [0, 12, 20, 30,100]

# Create the names for the four bins
group_names = ['Kid (<12)', 'Teenager (12-20)', 'Young (21-30)', 'Adult (>30)']

#Applying bins into main data frame
df_heroes["Age Ranges"] = pd.cut(df_heroes["Age"], bins, labels=group_names)

#With above dataframe (heroes), aggregating by Age Ranges
df_heroes_ranges_data=df_heroes.groupby(["Age Ranges"])

#Aggregating by price
revenue_ranges = df_heroes_ranges_data["Price"].sum()

#Count values per age ranges
ranges_counts = df_heroes["Age Ranges"].value_counts()

#Data frame to storage values
df_age_demographics = pd.DataFrame({"Purchase Count":ranges_counts,
                                   "Total Purchase Value":revenue_ranges})

#Applying format
pd.options.display.float_format = '${:,.2f}'.format

#Create a new column with average purchase price, sorting and showing results
df_age_demographics["Average Purchase Price"] = df_age_demographics["Total Purchase Value"] /  df_age_demographics["Purchase Count"] 
df_age_demographics = df_age_demographics.sort_values("Total Purchase Value",ascending=False)
df_age_demographics
```




<div>
<style>
    .dataframe thead tr:only-child th {
        text-align: right;
    }

    .dataframe thead th {
        text-align: left;
    }

    .dataframe tbody tr th {
        vertical-align: top;
    }
</style>
<table border="1" class="dataframe">
  <thead>
    <tr style="text-align: right;">
      <th></th>
      <th>Purchase Count</th>
      <th>Total Purchase Value</th>
      <th>Average Purchase Price</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>Young (21-30)</th>
      <td>418</td>
      <td>$1,233.62</td>
      <td>$2.95</td>
    </tr>
    <tr>
      <th>Teenager (12-20)</th>
      <td>269</td>
      <td>$764.86</td>
      <td>$2.84</td>
    </tr>
    <tr>
      <th>Adult (&gt;30)</th>
      <td>117</td>
      <td>$350.58</td>
      <td>$3.00</td>
    </tr>
    <tr>
      <th>Kid (&lt;12)</th>
      <td>54</td>
      <td>$165.37</td>
      <td>$3.06</td>
    </tr>
  </tbody>
</table>
</div>




```python
# Top Spenders
# Identify the the top 5 spenders in the game by total purchase value, then list (in a table):
# SN
# Purchase Count
# Average Purchase Price
# Total Purchase Value

#Data frame by Player(SN)
df_top_spenders_data=df_heroes.groupby(["Player"])

#Aggregating by price
revenue_spenders = df_top_spenders_data["Price"].sum()


#Count values per player
spenders_counts = df_heroes["Player"].value_counts()


#Data frame to storage values
df_top_spenders = pd.DataFrame({"Purchase Count":spenders_counts,
                                   "Total Purchase Value":revenue_spenders})


#Create a new column with average purchase price, sorting and showing results
df_top_spenders["Average Purchase Price"] = df_top_spenders["Total Purchase Value"] /  df_top_spenders["Purchase Count"] 
df_top_spenders = df_top_spenders.sort_values("Total Purchase Value",ascending=False)
df_top_spenders[["Purchase Count","Average Purchase Price","Total Purchase Value"]].head(5)
```




<div>
<style>
    .dataframe thead tr:only-child th {
        text-align: right;
    }

    .dataframe thead th {
        text-align: left;
    }

    .dataframe tbody tr th {
        vertical-align: top;
    }
</style>
<table border="1" class="dataframe">
  <thead>
    <tr style="text-align: right;">
      <th></th>
      <th>Purchase Count</th>
      <th>Average Purchase Price</th>
      <th>Total Purchase Value</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>Undirrala66</th>
      <td>5</td>
      <td>$3.41</td>
      <td>$17.06</td>
    </tr>
    <tr>
      <th>Aerithllora36</th>
      <td>4</td>
      <td>$3.77</td>
      <td>$15.10</td>
    </tr>
    <tr>
      <th>Saedue76</th>
      <td>4</td>
      <td>$3.39</td>
      <td>$13.56</td>
    </tr>
    <tr>
      <th>Sondim43</th>
      <td>4</td>
      <td>$3.25</td>
      <td>$13.02</td>
    </tr>
    <tr>
      <th>Mindimnya67</th>
      <td>4</td>
      <td>$3.18</td>
      <td>$12.74</td>
    </tr>
  </tbody>
</table>
</div>




```python
# Most Popular Items
# Identify the 5 most popular items by purchase count, then list (in a table):
# Item ID
# Item Name
# Purchase Count
# Item Price
# Total Purchase Value

#Data frame by Item Name(SN) with multiple aggregating 
df_popular_items_data= df_heroes.groupby(['Item Name'])['Price'].agg(['sum','count','mean'])

#Data frame to storage values
df_popular_items = pd.DataFrame({"Purchase Count":df_popular_items_data["count"],
                                   "Total Purchase Value":df_popular_items_data["sum"],
                                     "Avg Item Value":df_popular_items_data["mean"]}) 

#sorting and showing results (top 5)
df_popular_items.sort_values("Purchase Count",ascending=False).head(5)

```




<div>
<style>
    .dataframe thead tr:only-child th {
        text-align: right;
    }

    .dataframe thead th {
        text-align: left;
    }

    .dataframe tbody tr th {
        vertical-align: top;
    }
</style>
<table border="1" class="dataframe">
  <thead>
    <tr style="text-align: right;">
      <th></th>
      <th>Avg Item Value</th>
      <th>Purchase Count</th>
      <th>Total Purchase Value</th>
    </tr>
    <tr>
      <th>Item Name</th>
      <th></th>
      <th></th>
      <th></th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>Final Critic</th>
      <td>$2.76</td>
      <td>14</td>
      <td>$38.60</td>
    </tr>
    <tr>
      <th>Arcane Gem</th>
      <td>$2.45</td>
      <td>12</td>
      <td>$29.34</td>
    </tr>
    <tr>
      <th>Stormcaller</th>
      <td>$3.35</td>
      <td>12</td>
      <td>$40.19</td>
    </tr>
    <tr>
      <th>Betrayal, Whisper of Grieving Widows</th>
      <td>$2.35</td>
      <td>11</td>
      <td>$25.85</td>
    </tr>
    <tr>
      <th>Trickster</th>
      <td>$2.32</td>
      <td>10</td>
      <td>$23.22</td>
    </tr>
  </tbody>
</table>
</div>




```python
# Most Profitable Items
# Identify the 5 most profitable items by total purchase value, then list (in a table):
# Item ID
# Item Name
# Purchase Count
# Item Price
# Total Purchase Value

#Same dataframe above
#sorting and showing results (top 5)
df_popular_items.sort_values("Total Purchase Value",ascending=False).head(5)
```




<div>
<style>
    .dataframe thead tr:only-child th {
        text-align: right;
    }

    .dataframe thead th {
        text-align: left;
    }

    .dataframe tbody tr th {
        vertical-align: top;
    }
</style>
<table border="1" class="dataframe">
  <thead>
    <tr style="text-align: right;">
      <th></th>
      <th>Avg Item Value</th>
      <th>Purchase Count</th>
      <th>Total Purchase Value</th>
    </tr>
    <tr>
      <th>Item Name</th>
      <th></th>
      <th></th>
      <th></th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>Stormcaller</th>
      <td>$3.35</td>
      <td>12</td>
      <td>$40.19</td>
    </tr>
    <tr>
      <th>Final Critic</th>
      <td>$2.76</td>
      <td>14</td>
      <td>$38.60</td>
    </tr>
    <tr>
      <th>Retribution Axe</th>
      <td>$4.14</td>
      <td>9</td>
      <td>$37.26</td>
    </tr>
    <tr>
      <th>Splitter, Foe Of Subtlety</th>
      <td>$3.67</td>
      <td>9</td>
      <td>$33.03</td>
    </tr>
    <tr>
      <th>Spectral Diamond Doomblade</th>
      <td>$4.25</td>
      <td>7</td>
      <td>$29.75</td>
    </tr>
  </tbody>
</table>
</div>


