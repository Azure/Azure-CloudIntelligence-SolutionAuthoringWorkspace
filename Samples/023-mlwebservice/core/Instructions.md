*ML Web Service endpoint*: {Outputs.endpoint}

NOTE: In the beginning, the Web service will need about 10 minutes to boot.

#### Test command

```
curl -X POST -H "Content-Type: application/json" {Outputs.endpoint} -d <JSON payload>
```
