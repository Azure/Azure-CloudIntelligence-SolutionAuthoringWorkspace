$sitePackagesDirectory = 'site-packages'
py -m pip install -t "$sitePackagesDirectory" -r requirements.txt
$env:PYTHONPATH = $(Resolve-Path -Path "$sitePackagesDirectory").Path
py twitterClient.py
