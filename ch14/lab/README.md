## Lab Solution

Set some variables:

```
$REGION='us-east1'
$PROJECT='diamol2elabs'
$REGISTRY="${REGION}-docker.pkg.dev"
```

Create a new project and link your billing account:

```
gcloud projects create $PROJECT

gcloud billing projects link $PROJECT --billing-account=<your-billing-account-id>
```

Enable Artifact Registry, Cloud Run and Cloud Build services:

```
gcloud services enable artifactregistry.googleapis.com cloudbuild.googleapis.com run.googleapis.com --project=$PROJECT
```

Create a repo in the registry:

```
gcloud artifacts repositories create pi --repository-format=docker --project=$PROJECT --location=$REGION
```

Switch to the source code repo and submit the build:

```
cd ch14/exercises/pi-web

gcloud builds submit --tag="$REGISTRY/$PROJECT/pi/web" --project=$PROJECT 
```

Run with 8 CPUs:

```
gcloud run deploy pi-web --image="$REGISTRY/$PROJECT/pi/web" --cpu=8 --memory=4Gi --port=80 --allow-unauthenticated --project=$PROJECT --region=$REGION 

```

For a single request, 8 CPUs isn't much faster (13.5s compared to 15.3s):

![](/ch14/lab/img/my-pi.png)

But with multiple concurrent requests, the single core version would slow down and this version would keep going.

And delete the project:

```
gcloud projects delete $PROJECT --quiet
```