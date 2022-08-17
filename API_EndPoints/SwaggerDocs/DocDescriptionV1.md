## Endpoints
### Internal endpoints
These apis are mainly used by Machine, Intelligent Tool, Material widgets.
To be usable, the caller's access token request must contain the tenant information.

### Third party intergration endpoints
The Customer Area provides Service-To-Service (STS) Apis for third party intergration. 
There are 2 supported variants:
- A STS Api starts with **`/sts/`** and contains the tenant Id **`{tenantId}`** as the route parameter. E.g: `/sts/machines/{tenantId}` is the STS Api to get all machines of a specified tenant, or `​/sts​/machines​/{tenantId}​/{machineId}​/{status}` is to get machine details by its Id.
- A STS Api starts with **`/sts/`**, contains the sales unit Id **`{salesUnitId}`** and customer number **`{customerNumber}`** as the route parameters. E.g: `​/sts​/salesorgs​/{salesUnitId}​/customers​/{customerNumber}​/machines` is the STS Api to get all machines of combined parameters sales unit id and customer number.

## Scopes
### Main scopes
To be able to use the apis in the Customer Area, the caller's access token must have scope information of `ca`.

### Machine Scopes
Each machine scope defines various contrains for machine entity such as required fields, allowed machine sets. Then clients with corresponding scope must follow in order to create or the update machine entity successfully.
Supported machine scopes: `Generic` `Iot` `Cd2C` `Tulip`

### Client Scopes
Each STS Api requires one or more client scopes. E.g: To create the Installed machine, the client token must contain the scope `machines:full`. 

The scope convention is `{enityconvention}:{read|full}` where:
- `read` means only allow reading
- `full` means allow all actions reading/creating/updating/deleting

Scopes of Apis related to Intelligent Tool:
- `itools:full`
- `itools:read`

Scopes of Apis related to Material:
- `materials:full`
- `materials:read`

Scopes of Apis related to Assembly:
- `assemblies:full`
- `assemblies:read`

Scopes of Apis related to My/Available/Installed Machine:
- `machines:full`
- `machines:read`

Template Machine is special, only client has the scope `templates:full` can modify it.