def pipelines = [
    [name: 'diamol/ch02-hello-diamol', scriptPath: 'ch02/exercises/hello-diamol/Jenkinsfile'],
    [name: 'diamol/ch02-hello-diamol-web', scriptPath: 'ch02/exercises/hello-diamol-web/Jenkinsfile'],
    [name: 'diamol/ch03-web-ping', scriptPath: 'ch03/exercises/web-ping/Jenkinsfile'],
    [name: 'diamol/ch03-web-ping-optimized', scriptPath: 'ch03/exercises/web-ping-optimized/Jenkinsfile'],
    [name: 'diamol/ch03-lab', scriptPath: 'ch03/lab/Jenkinsfile'],
	[name: 'diamol/ch04-access-log', scriptPath: 'ch04/exercises/access-log/Jenkinsfile'],
	[name: 'diamol/ch04-image-gallery', scriptPath: 'ch04/exercises/image-gallery/Jenkinsfile'],
	[name: 'diamol/ch04-image-of-the-day', scriptPath: 'ch04/exercises/image-of-the-day/Jenkinsfile'],
	[name: 'diamol/ch04-multi-stage', scriptPath: 'ch04/exercises/multi-stage/Jenkinsfile'],
	[name: 'diamol/ch04-lab', scriptPath: 'ch04/lab/Jenkinsfile'],
	[name: 'diamol/ch04-lab-optimized', scriptPath: 'ch04/lab/Jenkinsfile.optimized'],
	[name: 'diamol/ch06-bind-mount', scriptPath: 'ch06/exercises/bind-mount/Jenkinsfile'],
	[name: 'diamol/ch06-file-display', scriptPath: 'ch06/exercises/file-display/Jenkinsfile'],
	[name: 'diamol/ch06-random-number', scriptPath: 'ch06/exercises/random-number/Jenkinsfile'],
	[name: 'diamol/ch06-todo-list', scriptPath: 'ch06/exercises/todo-list/Jenkinsfile'],
	[name: 'diamol/ch06-todo-list-v2', scriptPath: 'ch06/exercises/todo-list-v2/Jenkinsfile'],
	[name: 'diamol/ch06-lab', scriptPath: 'ch06/lab/Jenkinsfile'],
	[name: 'diamol/ch08-lab', scriptPath: 'ch08/lab/Jenkinsfile'],
	[name: 'diamol/ch08-lab-solution', scriptPath: 'ch08/lab/Jenkinsfile.solution'],
	[name: 'diamol/ch08-numbers-api', scriptPath: 'ch08/exercises/numbers/numbers-api/Jenkinsfile'],
	[name: 'diamol/ch08-numbers-api-v2', scriptPath: 'ch08/exercises/numbers/numbers-api/Jenkinsfile.v2'],
	[name: 'diamol/ch08-numbers-api-v3', scriptPath: 'ch08/exercises/numbers/numbers-api/Jenkinsfile.v3'],
	[name: 'diamol/ch08-numbers-web', scriptPath: 'ch08/exercises/numbers/numbers-web/Jenkinsfile'],
	[name: 'diamol/ch08-numbers-web-v2', scriptPath: 'ch08/exercises/numbers/numbers-web/Jenkinsfile.v2'],
	[name: 'diamol/ch08-numbers-web-v3', scriptPath: 'ch08/exercises/numbers/numbers-web/Jenkinsfile.v3'],
	[name: 'diamol/ch09-access-log', scriptPath: 'ch09/exercises/access-log/Jenkinsfile'],
	[name: 'diamol/ch09-grafana', scriptPath: 'ch09/exercises/grafana/Jenkinsfile'],
	[name: 'diamol/ch09-image-gallery', scriptPath: 'ch09/exercises/image-gallery/Jenkinsfile'],
	[name: 'diamol/ch09-image-of-the-day', scriptPath: 'ch09/exercises/image-of-the-day/Jenkinsfile'],
	[name: 'diamol/ch09-prometheus', scriptPath: 'ch09/exercises/prometheus/Jenkinsfile'],
	[name: 'diamol/ch09-todo-list', scriptPath: 'ch09/exercises/todo-list/Jenkinsfile'],
	[name: 'diamol/ch09-lab-grafana', scriptPath: 'ch09/lab/grafana/Jenkinsfile'],
	[name: 'diamol/ch09-lab-prometheus', scriptPath: 'ch09/lab/prometheus/Jenkinsfile'],
	[name: 'diamol/ch11-numbers-api', scriptPath: 'ch11/exercises/numbers/numbers-api/Jenkinsfile'],
	[name: 'diamol/ch11-numbers-web', scriptPath: 'ch11/exercises/numbers/numbers-web/Jenkinsfile'],
	[name: 'diamol/ch12-timecheck-1', scriptPath: 'ch12/exercises/timecheck/Jenkinsfile-1.0'],
	[name: 'diamol/ch12-timecheck-2', scriptPath: 'ch12/exercises/timecheck/Jenkinsfile-2.0'],
	[name: 'diamol/ch14-numbers-api', scriptPath: 'ch14/exercises/numbers/numbers-api/Jenkinsfile'],
	[name: 'diamol/ch14-numbers-web', scriptPath: 'ch14/exercises/numbers/numbers-web/Jenkinsfile'],
	[name: 'diamol/ch15-timecheck-3', scriptPath: 'ch15/exercises/timecheck/Jenkinsfile-3.0'],
	[name: 'diamol/apache', scriptPath: 'images/apache/Jenkinsfile'],
	[name: 'diamol/base', scriptPath: 'images/base/Jenkinsfile'],
	[name: 'diamol/cert-generator', scriptPath: 'images/cert-generator/Jenkinsfile'],
	[name: 'diamol/dotnet-aspnet', scriptPath: 'images/dotnet/aspnet/Jenkinsfile'],
	[name: 'diamol/dotnet-runtime', scriptPath: 'images/dotnet/runtime/Jenkinsfile'],
	[name: 'diamol/dotnet-sdk', scriptPath: 'images/dotnet/sdk/Jenkinsfile'],
	[name: 'diamol/elasticsearch', scriptPath: 'images/elasticsearch/Jenkinsfile'],
	[name: 'diamol/fluentd', scriptPath: 'images/fluentd/Jenkinsfile'],
	[name: 'diamol/gogs', scriptPath: 'images/gogs/Jenkinsfile'],
	[name: 'diamol/golang', scriptPath: 'images/golang/Jenkinsfile'],
	[name: 'diamol/grafana', scriptPath: 'images/grafana/Jenkinsfile'],
	[name: 'diamol/jenkins', scriptPath: 'images/jenkins/Jenkinsfile'],
	[name: 'diamol/kibana', scriptPath: 'images/kibana/Jenkinsfile'],
	[name: 'diamol/maven', scriptPath: 'images/maven/Jenkinsfile'],
	[name: 'diamol/nats', scriptPath: 'images/nats/Jenkinsfile'],
	[name: 'diamol/nats-sub', scriptPath: 'images/nats-sub/Jenkinsfile'],
	[name: 'diamol/nginx', scriptPath: 'images/nginx/Jenkinsfile'],
	[name: 'diamol/node', scriptPath: 'images/node/10.16.0/Jenkinsfile'],
	[name: 'diamol/node-15', scriptPath: 'images/node/10.15.2/Jenkinsfile'],
	[name: 'diamol/openjdk', scriptPath: 'images/openjdk/jre/Jenkinsfile'],
	[name: 'diamol/openjdk-jdk', scriptPath: 'images/openjdk/jdk/Jenkinsfile'],
	[name: 'diamol/postgres-115', scriptPath: 'images/postgres/11.5/Jenkinsfile'],
	[name: 'diamol/postgres-116', scriptPath: 'images/postgres/11.6/Jenkinsfile'],
	[name: 'diamol/prometheus', scriptPath: 'images/prometheus/Jenkinsfile'],
	[name: 'diamol/pwd-tls-client', scriptPath: 'images/pwd-tls/Jenkinsfile.client'],
	[name: 'diamol/pwd-tls-server', scriptPath: 'images/pwd-tls/Jenkinsfile.server'],
	[name: 'diamol/redis', scriptPath: 'images/redis/Jenkinsfile'],
	[name: 'diamol/redis-cli', scriptPath: 'images/redis-cli/Jenkinsfile'],
	[name: 'diamol/registry', scriptPath: 'images/registry/Jenkinsfile'],
	[name: 'diamol/traefik', scriptPath: 'images/traefik/Jenkinsfile']
]

for(p in pipelines) {
	pipelineJob("${p.name}") {
	    definition {
	        cpsScm {
	            scm {
	                git {
	                  remote {
	                    name('github')
	                    url('https://github.com/sixeyed/diamol.git')
	                  }
	                  branch('master')
	                  extensions {
	                  	cloneOptions {
	                  	  shallow(true)
	                  	  depth(1)
						  noTags(true)
	                  	}
	                  }
	                }
	            }
	            scriptPath("${p.scriptPath}")
	        }
	    }
	    triggers {
	        cron('@weekly')
	    }
	}
}