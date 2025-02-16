## Lab Solution

Check the existing web app logs:

```
kubectl logs deploy/todo-web 
```

> None - the default config only prints error logs.

Here's the new ConfigMap:

- [todo-web-config-with-logging.yaml](/ch15/lab/todo-list/todo-web-config-with-logging.yaml)

Apply the update:
```
kubectl apply -f ch15/lab/todo-list
```
It can take 30+ seconds for the change to get pushed to the containers:

```
sleep 30

kubectl exec deploy/todo-web -- cat /app/config/logging.json
```

This app reloads config when files change. Use the app and you should see some logs:


```
curl http://localhost:8025/list

kubectl logs deploy/todo-web
```

Not all apps reload config files when they change. In that case you can restart the rollout, which replaces Pods with new ones from the same spec - they see the new config file contents when they start:

```
kubectl rollout restart deploy/todo-web
```