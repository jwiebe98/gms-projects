EXCEL_FILE_NAME = "rates.xlsx"
RATE_SCHEMA_FILE_NAME = "rate-schema.json"
HEALTH_AND_DENTAL_SHEET = "Health & Dental"
ASSUMPTION_LIFE_SHEET = "Assumption Life"

WRITE = "w"
OUTPUT_FILE = "output.json"

HEALTH = "HEALTH"
DENTAL = "DENTAL"

START = "START"
END = "END"

EFFECTIVE_DATE = "EFFECTIVE_DATE"
PROVINCE_RATES = "PROVINCE_RATES"

BC = "BC"
AB = "AB"
SK = "SK"
MB = "MB"
ON = "ON"
NS = "NS"
PE = "PE"
NL = "NL"
YK = "YK"
NT = "NT"

SINGLE = "SINGLE"
COUPLE = "COUPLE"
FAMILY = "FAMILY"
SILVER = "SILVER"
GOLD = "GOLD"
PLATINUM = "PLATINUM"

_500 = "_500"
_1000 = "_1000"
_1500 = "_1500"
_2000 = "_2000"

ASSUMPTION_LIFE_PRODUCTS = "ASSUMPTION_LIFE_PRODUCTS"
LIFE_INSURANCE = "LIFE_INSURANCE"
DEPENDANT_LIFE_INSURANCE = "DEPENDANT_LIFE_INSURANCE"
ACCIDENTAL_DEATH_AND_DISMEMBERMENT = "ACCIDENTAL_DEATH_AND_DISMEMBERMENT"
SECOND_MEDICAL_OPINION = "SECOND_MEDICAL_OPINION"
CRITICAL_ILLNESS = "CRITICAL_ILLNESS"
DEPENDANT_CRITICAL_ILLNESS = "DEPENDANT_CRITICAL_ILLNESS"

_5000 = "_5000"
_10000 = "_10000"
_25000 = "_25000"
_50000 = "_50000"
_1XSALARY = "_1XSALARY"
_5000_2500 = "_5000_2500"
_10000_5000 = "_10000_5000"
TRADITIONAL = "TRADITIONAL"
HIGH_SEVERITY = "HIGH_SEVERITY"
RATE = "RATE"
VOLUME = "VOLUME"
MINIMUM_LIVES = "MINIMUM_LIVES"

PROVINCE_COLUMNS = {BC: {START: 2, END: 5}, AB: {START: 5, END: 8}, SK: {START: 8, END: 11}, MB: {START: 11, END: 14}, ON: {START: 14, END: 17}, NS: {START: 17, END: 20}, PE: {START: 20, END: 23}, NL: {START: 23, END: 26}, YK: {START: 26, END: 29}, NT: {START: 29, END: 32}}

EMPLOYEE_TYPES = {SINGLE: 0, COUPLE: 1, FAMILY: 2}

HEALTH_ROWS = {SILVER: 2, GOLD: 3, PLATINUM: 4}

DENTAL_ROWS = {_500: {SILVER : 7, GOLD: 12, PLATINUM: 17}, _1000: {SILVER : 8, GOLD: 13, PLATINUM: 18} ,_1500: {SILVER : 9, GOLD: 14, PLATINUM: 19}, _2000: {SILVER : 10, GOLD: 15, PLATINUM: 20}}

LIFE_INSURANCE_ROWS = {_10000: 0, _25000: 1, _50000: 2, _1XSALARY: 3}
ACCIDENTAL_DEATH_AND_DISMEMBERMENT_ROWS = {_10000: 5, _25000: 6, _50000: 7, _1XSALARY: 8}
DEPENDANT_LIFE_INSURANCE_ROWS = {_5000_2500: 10, _10000_5000: 11}
SECOND_MEDICAL_OPINION_ROW = 13
CRITICAL_ILLNESS_ROWS = {TRADITIONAL: {_10000: 15, _25000: 16}, HIGH_SEVERITY: {_10000: 17, _25000: 18}}
DEPENDANT_CRITICAL_ILLNESS_ROWS = {TRADITIONAL: {_5000_2500: 20, _10000_5000: 21}, HIGH_SEVERITY: {_5000: 22, _10000: 23}}

ASSUMPTION_LIFE_COLUMNS = {VOLUME: 3, MINIMUM_LIVES: 4, RATE: 5}

ASSUMPTION_LIFE_CRITICAL_ILLNESS_PRODUCTS_LIST = {CRITICAL_ILLNESS: CRITICAL_ILLNESS_ROWS, DEPENDANT_CRITICAL_ILLNESS: DEPENDANT_CRITICAL_ILLNESS_ROWS}

ASSUMPTION_LIFE_PRODUCTS_LIST = {LIFE_INSURANCE: LIFE_INSURANCE_ROWS, ACCIDENTAL_DEATH_AND_DISMEMBERMENT: ACCIDENTAL_DEATH_AND_DISMEMBERMENT_ROWS, DEPENDANT_LIFE_INSURANCE: DEPENDANT_LIFE_INSURANCE_ROWS}