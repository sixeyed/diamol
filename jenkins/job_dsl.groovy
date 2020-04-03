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
	[name: 'diamol/kibana', scriptPath: 'images/kibana/Jenkinsfile']
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
	        cron('H H(1-8) * * *')
	    }
	}
}