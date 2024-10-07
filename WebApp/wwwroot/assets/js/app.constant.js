'use strict';

var alertStatus = Object.freeze({
    'Success': 'Success',
    'Error': 'Error',
    'Warning': 'Warning'
});

var masterDataType = Object.freeze({
    'DeviceType': 'DEVICE_TYPE',
    'DeviceCat': 'DEVICE_CAT',
    'ProductBrand': 'PRODUCT_BRAND',
    'ProductType': 'PRODUCT_TYPE',
    'Vendor': 'VENDOR',
    'Company': 'COMPANY',
    'Branch': 'BRANCH',
    'Division': 'DIVISION',
    'Department': 'DEPARTMENT',
    'JobGroup': 'JOB_GROUP',
    'JobTitle': 'JOB_TITLE'
});

var empStatus = Object.freeze({
    'Permanent': 'PERMANENT',
    'Trainee': 'TRAINEE',
    'Outsource': 'OUTSOURCE',
    'Resign': 'RESIGN',
    'Pension': 'PENSION'
});

var assetStatus = Object.freeze({
    'Available': 'AVAILABLE',
    'OnUser': 'ON_USER',
    'OnRepair': 'ON_REPAIR',
    'EndOfPeriod': 'END_PERIOD',
    'Purchased': 'PURCHASED',
    'BackToVendor': 'BACK_TO_VENDOR',
    'AssetLost': 'ASSET_LOST'
});

var distributor = Object.freeze({
    'ProductTN': 'PT. Traktor Nusantara',
    'ProductCompetitor': 'Competitor'
});
