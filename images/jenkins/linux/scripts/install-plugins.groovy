import jenkins.model.Jenkins;

pm = Jenkins.instance.pluginManager
uc = Jenkins.instance.updateCenter

["git", "credentials", "workflow-aggregator", "cloudbees-folder"].each {
    if (! pm.getPlugin(it)) {
    deployment = uc.getPlugin(it).deploy(true)
    deployment.get()
    }
}

// Jenkins.instance.restart()