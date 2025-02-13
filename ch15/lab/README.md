
```
kubectl logs deploy/todo-web 
```

- [todo-web-config-with-logging.yaml](ch15/lab/todo-list/todo-web-config-with-logging.yaml)

```
kubectl apply -f ch15/lab/todo-list

kubectl exec deploy/todo-web -- cat /app/config/logging.json
```

- app reloads; not all apps do:

```
k rollout restart deploy/todo-web
```