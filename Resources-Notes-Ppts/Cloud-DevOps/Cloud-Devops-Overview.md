# Cloud DevOps Overview

## Cloud

Cloud Computing: Servers (other computers that are not physically onsite) that are accessed via the internet,
and the software (can be APIs, databases, functions, file stores, etc) that run on them.

Advantages: Lower maintenance and upfront cost/burden, shifting the security burden onto the provider,
easier scalability (horizontal and vertical), network redundancy to ensure global availability...

Horizontal scaling: Adding more instances of the same offering/level

Vertical scaling: Adding more capacity/throughput/hardware to an existing instance.

SLA (Service Level Agreement): Contract defining maximum allowable downtime.

90%: 36~ days of annual downtime ("One nine")
99: 3.6~ days of annual downtime
99.999 5~ minutes of annual downtime

### 3 types of cloud models

Public cloud: Cloud computing purchased from and managed by a third party cloud provider. (AWS, GCP, Azure...)

Private: Cloud computing provided to members/employees of an organization, managed by that organization.

Hybrid cloud: Mix of the aforementioned types of cloud.

### Cloud Service Types

#### IaaS (Infrastructure as a Service): Provides infrastructure components on the cloud, like networking hardware, VMs, storage, etc

The user is responsible for managing things like the OS, any middleware (load balancers, etc), the apps being served, and data.

The platform manages the physical infrastructure, like the hardware, servers and physical networking required to serve these resources online.

#### PaaS (Platform as a Service): Provides a platform for building and deploying applications. Includes the tools and infrastructure required to do so

The user is responsible for the application and their data.

The platform manages all of the infrastructure, as well as things like OSs, databases, runtimes, and any needed software dependencies.

#### SaaS (Software as a Service): An application that users interact with over the internet, and not locally on their machines

The user is responsible for... not forgetting their log in details and paying on time every month.

The platform manages... everything. They manage the software being provided, any updates, maintenance, etc.

## DevOps

DevOps is a set of practices and associated tools to automate the integration/deployment of software.

CI (Continuous Integration) - Continuously merging new features/changes back into the main codebase.

CD (Continuous Delivery) - Automatic deployment of code changes to a test or production environment after the new version of the program is built. Still requires some human intervention to deploy the new build to end users.

CD (Continuous deployment) - Fully automated pipeline that deploys new code changes and releases that new version to the end customer. Nobody has to do anything, as long as the code passes all tests, it will make it to production and the users.
