import httplib as http
import urllib2
import json

account_name = "stgskx2h7bcta3de"
account_key = "3R4HLkPqO/JSVXZJ1OOBbGwxd4ner+B92q8jxRo/9ajDAicld02B3Zz4AL+8GU3ZXcA0mdPeubvdmiTaBwoRuw=="

authorization = "SharedKey %s:%s"%(account_name, account_key)

list_request = "https://%s.blob.core.windows.net/?comp=list"%account_name

headers = {
		'Authorization': authorization,
		"x-ms-version": "2015-02-21",
		"x-ms-date": "sd"
		}

request = urllib2.Request(list_request, None, headers)

response = urllib2.urlopen(request)

the_page = response.read()

print(the_page)