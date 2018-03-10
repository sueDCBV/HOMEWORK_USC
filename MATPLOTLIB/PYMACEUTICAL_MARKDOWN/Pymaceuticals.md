
Observable Trends
- From all 4 treatments (drugs), Capomulin is the only one which achieves to reduce tumor volume and develops slower metastatic.
- In spite of Infubinol has bad results in terms of "Tumor Volume Changes", in "Metastatic sites" has a significant better performance than Ketapril and Pacebo.
- About "Survival Rate", again Capomulin has very good results compare to other treatments, in this case the worst rate has Infubinol, we can see a meaningful decrease in timepoint 35 (20% less!) 


```python
# Dependencies
from matplotlib import pyplot as plt
import numpy as np
import pandas as pd
import seaborn as sns
import pprint
```


```python
# Read mice and drugs data
mouse_data = pd.read_csv("raw_data/mouse_drug_data.csv")
mouse_data.head()
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
      <th>Mouse ID</th>
      <th>Drug</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>0</th>
      <td>f234</td>
      <td>Stelasyn</td>
    </tr>
    <tr>
      <th>1</th>
      <td>x402</td>
      <td>Stelasyn</td>
    </tr>
    <tr>
      <th>2</th>
      <td>a492</td>
      <td>Stelasyn</td>
    </tr>
    <tr>
      <th>3</th>
      <td>w540</td>
      <td>Stelasyn</td>
    </tr>
    <tr>
      <th>4</th>
      <td>v764</td>
      <td>Stelasyn</td>
    </tr>
  </tbody>
</table>
</div>




```python
# Read clinical results
clinical_data = pd.read_csv("raw_data/clinicaltrial_data.csv")
clinical_data.head()
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
      <th>Mouse ID</th>
      <th>Timepoint</th>
      <th>Tumor Volume (mm3)</th>
      <th>Metastatic Sites</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>0</th>
      <td>b128</td>
      <td>0</td>
      <td>45.0</td>
      <td>0</td>
    </tr>
    <tr>
      <th>1</th>
      <td>f932</td>
      <td>0</td>
      <td>45.0</td>
      <td>0</td>
    </tr>
    <tr>
      <th>2</th>
      <td>g107</td>
      <td>0</td>
      <td>45.0</td>
      <td>0</td>
    </tr>
    <tr>
      <th>3</th>
      <td>a457</td>
      <td>0</td>
      <td>45.0</td>
      <td>0</td>
    </tr>
    <tr>
      <th>4</th>
      <td>c819</td>
      <td>0</td>
      <td>45.0</td>
      <td>0</td>
    </tr>
  </tbody>
</table>
</div>




```python
 # Merge two dataframes using an inner join
clinical_table = pd.merge(clinical_data,mouse_data, on="Mouse ID")
#Filtering results as instructions requirements 
clinical_table = clinical_table.loc[clinical_table['Drug'].isin(['Capomulin','Infubinol','Ketapril','Placebo'])]
clinical_table.head()
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
      <th>Mouse ID</th>
      <th>Timepoint</th>
      <th>Tumor Volume (mm3)</th>
      <th>Metastatic Sites</th>
      <th>Drug</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>0</th>
      <td>b128</td>
      <td>0</td>
      <td>45.000000</td>
      <td>0</td>
      <td>Capomulin</td>
    </tr>
    <tr>
      <th>1</th>
      <td>b128</td>
      <td>5</td>
      <td>45.651331</td>
      <td>0</td>
      <td>Capomulin</td>
    </tr>
    <tr>
      <th>2</th>
      <td>b128</td>
      <td>10</td>
      <td>43.270852</td>
      <td>0</td>
      <td>Capomulin</td>
    </tr>
    <tr>
      <th>3</th>
      <td>b128</td>
      <td>15</td>
      <td>43.784893</td>
      <td>0</td>
      <td>Capomulin</td>
    </tr>
    <tr>
      <th>4</th>
      <td>b128</td>
      <td>20</td>
      <td>42.731552</td>
      <td>0</td>
      <td>Capomulin</td>
    </tr>
  </tbody>
</table>
</div>




```python
#Group by Drug and Timepoint, aggregation average by Tumor Volume 
tumor_analysis= clinical_table.groupby(["Drug","Timepoint"])['Tumor Volume (mm3)'].agg(['mean']).sort_index().reset_index()
tumor_analysis= tumor_analysis.rename(columns={"mean":"Tumor Volume (mm3)"})
tumor_analysis.head()
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
      <th>Drug</th>
      <th>Timepoint</th>
      <th>Tumor Volume (mm3)</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>0</th>
      <td>Capomulin</td>
      <td>0</td>
      <td>45.000000</td>
    </tr>
    <tr>
      <th>1</th>
      <td>Capomulin</td>
      <td>5</td>
      <td>44.266086</td>
    </tr>
    <tr>
      <th>2</th>
      <td>Capomulin</td>
      <td>10</td>
      <td>43.084291</td>
    </tr>
    <tr>
      <th>3</th>
      <td>Capomulin</td>
      <td>15</td>
      <td>42.064317</td>
    </tr>
    <tr>
      <th>4</th>
      <td>Capomulin</td>
      <td>20</td>
      <td>40.716325</td>
    </tr>
  </tbody>
</table>
</div>




```python
#A simple scatter plot with pandas and matplot
tumor_analysis.plot(kind="scatter", x="Timepoint", y="Tumor Volume (mm3)",legend=True)
plt.ylim(30, 75)
plt.xlim(0, 50)
plt.xticks(np.arange(0, 50+1, 5.0))
plt.show()
```


![png](output_6_0.png)



```python
#Same scatter plot using SEABORN library

g = sns.FacetGrid(tumor_analysis, hue='Drug', margin_titles=True, size=7)

g.map(plt.scatter, 'Timepoint', 'Tumor Volume (mm3)', edgecolor="white", s=50, lw=1,marker="D").add_legend()
plt.ylim(30, 75)
plt.xlim(0, 50)
plt.xticks(np.arange(0, 50+1, 5.0))
g.ax.set_title('Tumor volume changes over time for each treatment (Drugs)')

g.map(sns.regplot, 'Timepoint', 'Tumor Volume (mm3)',marker="D");
sns.set_style("darkgrid")
plt.show()
```


![png](output_7_0.png)



```python
#Same scatter plot using ALTAIR AND VEGALITE library
#To run this results, it's necessary to install altair library and update jupyter-notebook
#Follow this steps: https://altair-viz.github.io/installation.html
#In Vega Editor contains tiptools for each value!
from vega3 import VegaLite

VegaLite({
    "title": "Tumor volume changes over time for each treatment (Drugs)",
    "mark": "point",
    "width": 500,
    "height": 400,
    "encoding": {
        "x": {"type": "quantitative","field": "Timepoint", "ticks":True},
        "y": {"type": "quantitative","field": "Tumor Volume (mm3)","scale": {"domain": [30,75]}, "ticks":True,
              "tooltip": {"field": "Tumor Volume (mm3)", "type": "quantitative"}},
        "color": {"field": "Drug", "type": "nominal"},
        "shape": {"field": "Drug", "type": "nominal"},
}
}, tumor_analysis)
```


<div class="vega-embed" id="4c9ffcbe-cef4-4214-a023-d0b8a234e2de"></div>

<style>
.vega-embed svg, .vega-embed canvas {
  border: 1px dotted gray;
}

.vega-embed .vega-actions a {
  margin-right: 6px;
}
</style>






![png](output_8_2.png)



```python
#Scatter plots, error bars
# Create a bunch of samples, each with div items(5)
div = 5
lim = len(tumor_analysis) // div
samples = [tumor_analysis.iloc[(i * div):(i * div + div), 2]
           for i in range(0, lim)]

# Calculate means
means = [s.mean() for s in samples]

# Calculate standard error on means
sem = [s.sem() for s in samples]

# Plot sample means with error bars
fig, ax = plt.subplots()

ax.errorbar(np.arange(0, len(means)), means, yerr=sem, fmt="o", color="b",
            alpha=0.5, label="Mean of Tumor Volume (mm3)")

ax.set_xlim(-0.5, len(means))

ax.set_xlabel("Sample Number")
ax.set_ylabel("Mean of Tumor Volume (mm3)")

plt.legend(loc="best", fontsize="small", fancybox=True)
plt.show()
```


![png](output_9_0.png)



```python
#Group by Drug and Timepoint, aggregation average by Metastatic Sites
metastatic_analysis= clinical_table.groupby(["Drug","Timepoint"])['Metastatic Sites'].agg(['mean']).sort_index().reset_index()
metastatic_analysis= metastatic_analysis.rename(columns={"mean":"Metastatic Sites"})
metastatic_analysis.head()
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
      <th>Drug</th>
      <th>Timepoint</th>
      <th>Metastatic Sites</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>0</th>
      <td>Capomulin</td>
      <td>0</td>
      <td>0.000000</td>
    </tr>
    <tr>
      <th>1</th>
      <td>Capomulin</td>
      <td>5</td>
      <td>0.160000</td>
    </tr>
    <tr>
      <th>2</th>
      <td>Capomulin</td>
      <td>10</td>
      <td>0.320000</td>
    </tr>
    <tr>
      <th>3</th>
      <td>Capomulin</td>
      <td>15</td>
      <td>0.375000</td>
    </tr>
    <tr>
      <th>4</th>
      <td>Capomulin</td>
      <td>20</td>
      <td>0.652174</td>
    </tr>
  </tbody>
</table>
</div>




```python
#A simple scatter plot with pandas and matplot
metastatic_analysis.plot(kind="scatter", x="Timepoint", y="Metastatic Sites",legend=True)
plt.ylim(0.0, 4.0)
plt.xlim(0, 50)
plt.xticks(np.arange(0, 50+1, 5.0))
plt.show()
```


![png](output_11_0.png)



```python
#Same scatter plot using SEABORN library

g = sns.FacetGrid(metastatic_analysis, hue='Drug', margin_titles=True, size=7)

g.map(plt.scatter, 'Timepoint', 'Metastatic Sites', edgecolor="white", s=50, lw=1,marker="D").add_legend()
plt.ylim(0.0, 4.0)
plt.xlim(0, 50)
plt.xticks(np.arange(0, 50+1, 5.0))
g.ax.set_title('Metastatic sites changes over time for each treatment')

g.map(sns.regplot, 'Timepoint', 'Metastatic Sites',marker="D");
sns.set_style("darkgrid")
plt.show()
```


![png](output_12_0.png)



```python
#Same scatter plot using ALTAIR AND VEGALITE library
#To run this results, it's necessary to install altair library and update jupyter-notebook
#Follow this steps: https://altair-viz.github.io/installation.html
#In Vega Editor contains tiptools for each value!

from vega3 import VegaLite

VegaLite({
    "title": "Metastatic sites changes over time for each treatment",
    "mark": "point",
    "width": 500,
    "height": 400,
    "encoding": {
        "x": {"type": "quantitative","field": "Timepoint", "ticks":True},
        "y": {"type": "quantitative","field": "Metastatic Sites","scale": {"domain": [0.0, 4.0]}, "ticks":True,
              "tooltip": {"field": "Metastatic Sites", "type": "quantitative"}},
        "color": {"field": "Drug", "type": "nominal"},
        "shape": {"field": "Drug", "type": "nominal"},
}
}, metastatic_analysis)
```


<div class="vega-embed" id="018da50a-2f82-47d7-bc1a-193386d410d0"></div>

<style>
.vega-embed svg, .vega-embed canvas {
  border: 1px dotted gray;
}

.vega-embed .vega-actions a {
  margin-right: 6px;
}
</style>






![png](output_13_2.png)



```python
#Scatter plots, error bars
# Create a bunch of samples, each with div items
div = 5
lim = len(metastatic_analysis) // div
samples = [metastatic_analysis.iloc[(i * div):(i * div + div), 2]
           for i in range(0, lim)]

# Calculate means
means = [s.mean() for s in samples]

# Calculate standard error on means
sem = [s.sem() for s in samples]

# Plot sample means with error bars
fig, ax = plt.subplots()

ax.errorbar(np.arange(0, len(means)), means, yerr=sem, fmt="o", color="b",
            alpha=0.5, label="Mean of Metastatic Sites")

ax.set_xlim(-0.5, len(means))

ax.set_xlabel("Sample Number")
ax.set_ylabel("Mean of Metastatic Sites")

plt.legend(loc="best", fontsize="small", fancybox=True)
plt.show()
```


![png](output_14_0.png)



```python
#Group by Drug and Timepoint, aggregation average by Mouse Count
mice_analysis= clinical_table.groupby(["Drug","Timepoint"])['Mouse ID'].agg(['count']).sort_index().reset_index()
mice_analysis= mice_analysis.rename(columns={"count":"Mouse Count"})
mice_analysis["Survival Rate"] = (mice_analysis["Mouse Count"]/25)*100 
mice_analysis.head()
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
      <th>Drug</th>
      <th>Timepoint</th>
      <th>Mouse Count</th>
      <th>Survival Rate</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>0</th>
      <td>Capomulin</td>
      <td>0</td>
      <td>25</td>
      <td>100.0</td>
    </tr>
    <tr>
      <th>1</th>
      <td>Capomulin</td>
      <td>5</td>
      <td>25</td>
      <td>100.0</td>
    </tr>
    <tr>
      <th>2</th>
      <td>Capomulin</td>
      <td>10</td>
      <td>25</td>
      <td>100.0</td>
    </tr>
    <tr>
      <th>3</th>
      <td>Capomulin</td>
      <td>15</td>
      <td>24</td>
      <td>96.0</td>
    </tr>
    <tr>
      <th>4</th>
      <td>Capomulin</td>
      <td>20</td>
      <td>23</td>
      <td>92.0</td>
    </tr>
  </tbody>
</table>
</div>




```python
#A simple scatter plot with pandas and matplot
mice_analysis.plot(kind="scatter", x="Timepoint", y="Survival Rate",legend=True)
plt.ylim(20, 100.0+9)
plt.xlim(0, 50)
plt.xticks(np.arange(0, 50+1, 5.0))
plt.show()
```


![png](output_16_0.png)



```python
#Same scatter plot using SEABORN library
g = sns.FacetGrid(mice_analysis, hue='Drug', margin_titles=True, size=7)

g.map(plt.scatter, 'Timepoint', 'Survival Rate', edgecolor="white", s=50, lw=1,marker="D").add_legend()
plt.ylim(20, 100.0+9)
plt.xlim(0, 50)
plt.xticks(np.arange(0, 50+1, 5.0))
g.ax.set_title('Survival rate through the course of treatment')

g.map(sns.regplot, 'Timepoint', 'Survival Rate',marker="D");
sns.set_style("darkgrid")
plt.show()
```


![png](output_17_0.png)



```python
#Same scatter plot using ALTAIR AND VEGALITE library
#To run this results, it's necessary to install altair library and update jupyter-notebook
#Follow this steps: https://altair-viz.github.io/installation.html
#In Vega Editor contains tiptools for each value!

from vega3 import VegaLite

VegaLite({
    "title": "Survival rate through the course of treatment",
    "mark": "point",
    "width": 500,
    "height": 400,
    "encoding": {
        "x": {"type": "quantitative","field": "Timepoint", "ticks":True},
        "y": {"type": "quantitative","field": "Survival Rate","scale": {"domain": [20, 100.0]}, "ticks":True,
              "tooltip": {"field": "Survival Rate", "type": "quantitative"}},
        "color": {"field": "Drug", "type": "nominal"},
        "shape": {"field": "Drug", "type": "nominal"},
}
}, mice_analysis)
```


<div class="vega-embed" id="e457c70d-179d-4850-94e7-4d2c1421c3f5"></div>

<style>
.vega-embed svg, .vega-embed canvas {
  border: 1px dotted gray;
}

.vega-embed .vega-actions a {
  margin-right: 6px;
}
</style>






![png](output_18_2.png)



```python
#Filter dataframe, timepoint = 45
tumor_analysis_evolution=tumor_analysis.loc[tumor_analysis['Timepoint'].isin(['45'])]
#Calculate Tumor Change
tumor_analysis_evolution["Tumor Change"] = ((tumor_analysis_evolution["Tumor Volume (mm3)"]-45) /45)*100
tumor_analysis_evolution
```

    c:\anaconda\envs\pydata\lib\site-packages\ipykernel_launcher.py:4: SettingWithCopyWarning: 
    A value is trying to be set on a copy of a slice from a DataFrame.
    Try using .loc[row_indexer,col_indexer] = value instead
    
    See the caveats in the documentation: http://pandas.pydata.org/pandas-docs/stable/indexing.html#indexing-view-versus-copy
      after removing the cwd from sys.path.
    




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
      <th>Drug</th>
      <th>Timepoint</th>
      <th>Tumor Volume (mm3)</th>
      <th>Tumor Change</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>9</th>
      <td>Capomulin</td>
      <td>45</td>
      <td>36.236114</td>
      <td>-19.475303</td>
    </tr>
    <tr>
      <th>19</th>
      <td>Infubinol</td>
      <td>45</td>
      <td>65.755562</td>
      <td>46.123472</td>
    </tr>
    <tr>
      <th>29</th>
      <td>Ketapril</td>
      <td>45</td>
      <td>70.662958</td>
      <td>57.028795</td>
    </tr>
    <tr>
      <th>39</th>
      <td>Placebo</td>
      <td>45</td>
      <td>68.084082</td>
      <td>51.297960</td>
    </tr>
  </tbody>
</table>
</div>




```python
#Bar plot, using colors for positive and negative values
tumor_analysis_evolution['positive'] = tumor_analysis_evolution['Tumor Change'] > 0
tumor_analysis_evolution['positive']

x_axis = np.arange(len(tumor_analysis_evolution))

plt.bar(x_axis, tumor_analysis_evolution["Tumor Change"], color=tumor_analysis_evolution.positive.map({True: 'r', False: 'g'}), alpha=0.5, align="edge")
tick_locations = [value+0.4 for value in x_axis]
plt.xticks(tick_locations, tumor_analysis_evolution["Drug"])

plt.xlim(-0.25, len(x_axis))
plt.ylim(-25, 70)

plt.title("% tumor volume change for each drug across the full 45 days")
plt.xlabel("Drug")
plt.ylabel("% Tumor Volume Change")

plt.show()
```

    c:\anaconda\envs\pydata\lib\site-packages\ipykernel_launcher.py:2: SettingWithCopyWarning: 
    A value is trying to be set on a copy of a slice from a DataFrame.
    Try using .loc[row_indexer,col_indexer] = value instead
    
    See the caveats in the documentation: http://pandas.pydata.org/pandas-docs/stable/indexing.html#indexing-view-versus-copy
      
    


![png](output_20_1.png)

