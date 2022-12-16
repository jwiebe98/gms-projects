import constants as c

import numpy as np
import pandas as pd

import json
from datetime import datetime


def GetPrices(province, ratesDictionary):

    provinceName = province.head().columns[0]

    for employeeType in c.EMPLOYEE_TYPES:

        for healthTier in c.HEALTH_ROWS:

            ratesDictionary[c.PROVINCE_RATES][provinceName][c.HEALTH][healthTier][employeeType] = province.iat[c.HEALTH_ROWS[healthTier], c.EMPLOYEE_TYPES[employeeType]]

        for dentalCoverageAmount in c.DENTAL_ROWS:

            for dentalTier in c.DENTAL_ROWS[dentalCoverageAmount]:

                ratesDictionary[c.PROVINCE_RATES][provinceName][c.DENTAL][dentalCoverageAmount][dentalTier][employeeType] = province.iat[c.DENTAL_ROWS[dentalCoverageAmount][dentalTier], c.EMPLOYEE_TYPES[employeeType]]


excelFile = pd.read_excel(c.EXCEL_FILE_NAME, sheet_name=None)

ratesDictionary = {}
with open(c.RATE_SCHEMA_FILE_NAME) as json_file:
    ratesDictionary = json.load(json_file)

for provinceColumn in c.PROVINCE_COLUMNS:

    start = c.PROVINCE_COLUMNS[provinceColumn][c.START]

    end = c.PROVINCE_COLUMNS[provinceColumn][c.END]

    provinceData = excelFile[c.HEALTH_AND_DENTAL_SHEET].iloc[:,start:end]
    GetPrices(provinceData, ratesDictionary)

for assumptionLifeProduct in c.ASSUMPTION_LIFE_PRODUCTS_LIST:
    for coverageAmount in c.ASSUMPTION_LIFE_PRODUCTS_LIST[assumptionLifeProduct]:
        ratesDictionary[c.ASSUMPTION_LIFE_PRODUCTS][assumptionLifeProduct][coverageAmount][c.VOLUME] = excelFile[c.ASSUMPTION_LIFE_SHEET].iat[c.ASSUMPTION_LIFE_PRODUCTS_LIST[assumptionLifeProduct][coverageAmount], c.ASSUMPTION_LIFE_COLUMNS[c.VOLUME]]
        ratesDictionary[c.ASSUMPTION_LIFE_PRODUCTS][assumptionLifeProduct][coverageAmount][c.RATE] = excelFile[c.ASSUMPTION_LIFE_SHEET].iat[c.ASSUMPTION_LIFE_PRODUCTS_LIST[assumptionLifeProduct][coverageAmount], c.ASSUMPTION_LIFE_COLUMNS[c.RATE]]
        ratesDictionary[c.ASSUMPTION_LIFE_PRODUCTS][assumptionLifeProduct][coverageAmount][c.MINIMUM_LIVES] = excelFile[c.ASSUMPTION_LIFE_SHEET].iat[c.ASSUMPTION_LIFE_PRODUCTS_LIST[assumptionLifeProduct][coverageAmount], c.ASSUMPTION_LIFE_COLUMNS[c.MINIMUM_LIVES]]


ratesDictionary[c.ASSUMPTION_LIFE_PRODUCTS][c.SECOND_MEDICAL_OPINION][c.RATE] = excelFile[c.ASSUMPTION_LIFE_SHEET].iat[c.SECOND_MEDICAL_OPINION_ROW, c.ASSUMPTION_LIFE_COLUMNS[c.RATE]]
ratesDictionary[c.ASSUMPTION_LIFE_PRODUCTS][c.SECOND_MEDICAL_OPINION][c.MINIMUM_LIVES] = excelFile[c.ASSUMPTION_LIFE_SHEET].iat[c.SECOND_MEDICAL_OPINION_ROW, c.ASSUMPTION_LIFE_COLUMNS[c.MINIMUM_LIVES]]

for ciProduct in c.ASSUMPTION_LIFE_CRITICAL_ILLNESS_PRODUCTS_LIST:
    for coverageTier in c.ASSUMPTION_LIFE_CRITICAL_ILLNESS_PRODUCTS_LIST[ciProduct]:
        for coverageAmount in c.ASSUMPTION_LIFE_CRITICAL_ILLNESS_PRODUCTS_LIST[ciProduct][coverageTier]:
            ratesDictionary[c.ASSUMPTION_LIFE_PRODUCTS][ciProduct][coverageTier][coverageAmount][c.VOLUME] = excelFile[c.ASSUMPTION_LIFE_SHEET].iat[c.ASSUMPTION_LIFE_CRITICAL_ILLNESS_PRODUCTS_LIST[ciProduct][coverageTier][coverageAmount], c.ASSUMPTION_LIFE_COLUMNS[c.VOLUME]]
            ratesDictionary[c.ASSUMPTION_LIFE_PRODUCTS][ciProduct][coverageTier][coverageAmount][c.RATE] = excelFile[c.ASSUMPTION_LIFE_SHEET].iat[c.ASSUMPTION_LIFE_CRITICAL_ILLNESS_PRODUCTS_LIST[ciProduct][coverageTier][coverageAmount], c.ASSUMPTION_LIFE_COLUMNS[c.RATE]]
            ratesDictionary[c.ASSUMPTION_LIFE_PRODUCTS][ciProduct][coverageTier][coverageAmount][c.MINIMUM_LIVES] = excelFile[c.ASSUMPTION_LIFE_SHEET].iat[c.ASSUMPTION_LIFE_CRITICAL_ILLNESS_PRODUCTS_LIST[ciProduct][coverageTier][coverageAmount], c.ASSUMPTION_LIFE_COLUMNS[c.MINIMUM_LIVES]]

ratesDictionary[c.EFFECTIVE_DATE] = excelFile[c.HEALTH_AND_DENTAL_SHEET].head().columns[0].strftime('%d-%m-%Y')

json_object = json.dumps(ratesDictionary, indent=4)

with open(c.OUTPUT_FILE, c.WRITE) as outfile:
    outfile.write(json_object)