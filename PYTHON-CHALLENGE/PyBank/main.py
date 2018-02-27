import os
import csv
import datetime
import glob
import pprint
import itertools
import operator

# Lists to store data
ls_nro_months = []
ls_periodos = []
ls_changes = []
ls_revenue = []
ls_zip=[]

#initialize variables
fl_total_revenue = 0
fl_increase = 0.0

#Dictionary for print and export results
dic_results = {}

#Goes into an entire path and brings each file
path = 'raw_data'
for file_pyBankCSV in glob.glob( os.path.join(path, '*.*') ):
  
    # Read in the CSV file
    with open(file_pyBankCSV, newline="") as csvfile:
        next(csvfile, None)
        csvreader = csv.reader(csvfile, delimiter=',')
        
        # Loop through the data
        for row in csvreader:
            #Change datatype into datetime, this is important for sorting like dates and aggregating data
            dt_periodo = datetime.datetime.strptime(row[0],"%b-%y") 
            #This list in only for months (because it's required for printing)
            ls_nro_months.append(str(dt_periodo.month))
            ls_periodos.append(dt_periodo.strftime("%b-%y"))                    
            ls_revenue.append(row[1])

#Now all data of each file is stored in lists

#Obtaining a list with unique months (because it's required for printing)
ls_nro_months_uq = list(set(ls_nro_months))
int_total_months = len(ls_nro_months_uq) 
int_total_period = len(ls_nro_months)

#Joining lists into tuple
ls_zip=tuple(zip(ls_periodos,ls_revenue))

#Sorting list descending by periods (month-year)
###This is required for two things: 
###Agregating revenue of same periods and calculate increase/decrease between periods: 
###Aggregating revenue: For example, Jan-16 is appears in each file, 
###before processing data, we need to aggregate revenue of these periods in 1 period

ls_zip=sorted(ls_zip, key=lambda x: datetime.datetime.strptime(x[0], '%b-%y'))

#Function for accumulation
def accumulate(l):
    it = itertools.groupby(l, operator.itemgetter(0))
    for key, subiter in it:
       yield key, sum(float(item[1]) for item in subiter) 

#Calling function above, now we have a list with periods and revenue aggregated:
#No repeated periods (month-year)
ls_zip=list(accumulate(ls_zip))

#Unzip list for calculating changes
ls_periods_agg, ls_revenue_agg = zip(*ls_zip)

# Calculating changes and storage them in a list
for x in ls_revenue_agg:
    fl_total_revenue = fl_total_revenue  + float(x)

for x in range(1,len(ls_revenue_agg)):
    dife=float(ls_revenue_agg[x])-float(ls_revenue_agg[x-1])
    ls_changes.append(dife)

#First period doesn't have change
ls_changes.insert(0,0)

#Obtaining average revenue change
fl_increase= float(sum(ls_changes))/float(len(ls_changes))

#Joining periods and changes, this is necesarry for calculating max and min    
ls_zip_final=tuple(zip(ls_periods_agg,ls_changes))

#Dictionary with final results
dic_results = {"Total Months": int_total_months,
                "Total Periods (Month-Year)": len(ls_periods_agg),
                "Total Revenue": fl_total_revenue,
                "Average Revenue Change": fl_increase,
                "Greatest Increase in Revenue": max(ls_zip_final,key=lambda x:x[1]),
                "Greatest Decrease in Revenue":min(ls_zip_final,key=lambda x:x[1])}

#Print in terminal
print("\nFinancial Analysis:")
for key, car in dic_results.items():
    print(key,car)

#Export results into a CSV
#Create new CSV
newPyBankCSV  = os.path.join('','results', 'Results.csv')

with open(newPyBankCSV, 'w') as f:
    writer = csv.writer(f)
    writer.writerow(["Financial Analysis"])
    [f.write('{0},{1}\n'.format(key, value)) for key, value in dic_results.items()]
