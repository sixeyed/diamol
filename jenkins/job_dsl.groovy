def pipelines = [
    [name: 'diamol/ch02-hello-diamol', scriptPath: 'ch02/exercises/hello-diamol/Jenkinsfile'],
    [name: 'diamol/ch02-hello-diamol-web', scriptPath: 'ch02/exercises/hello-diamol-web/Jenkinsfile'],
    [name: 'diamol/ch03-web-ping', scriptPath: 'ch03/exercises/web-ping/Jenkinsfile'],
    [name: 'diamol/ch03-web-ping-optimized', scriptPath: 'ch03/exercises/web-ping-optimized/Jenkinsfile'],
    [name: 'diamol/ch03-lab', scriptPath: 'ch03/lab/Jenkinsfile']    
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