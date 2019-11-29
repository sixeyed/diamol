import jenkins.model.*
import com.cloudbees.plugins.credentials.*
import com.cloudbees.plugins.credentials.common.*
import com.cloudbees.plugins.credentials.domains.*
import com.cloudbees.plugins.credentials.impl.*
import com.cloudbees.jenkins.plugins.sshcredentials.impl.*
import org.jenkinsci.plugins.plaincredentials.*
import org.jenkinsci.plugins.plaincredentials.impl.*
import hudson.util.Secret
import hudson.plugins.sshslaves.*
import org.apache.commons.fileupload.* 
import org.apache.commons.fileupload.disk.*
import java.nio.file.Files

domain = Domain.global()
store = Jenkins.instance.getExtensionList('com.cloudbees.plugins.credentials.SystemCredentialsProvider')[0].getStore()

factory = new DiskFileItemFactory()
dfi = factory.createItem("", "application/octet-stream", false, "ca.pem")
out = dfi.getOutputStream()
file = new File("/certs/ca.pem")
Files.copy(file.toPath(), out)
caFile = new FileCredentialsImpl(CredentialsScope.GLOBAL,"docker-ca.pem","Docker Engine CA cert", dfi,"","")

dfi = factory.createItem("", "application/octet-stream", false, "client-cert.pem")
out = dfi.getOutputStream()
file = new File("/certs/client-cert.pem")
Files.copy(file.toPath(), out)
certFile = new FileCredentialsImpl(CredentialsScope.GLOBAL,"docker-cert.pem","Docker Engine client cert", dfi,"","")

dfi = factory.createItem("", "application/octet-stream", false, "client-key.pem")
out = dfi.getOutputStream()
file = new File("/certs/client-key.pem")
Files.copy(file.toPath(), out)
keyFile = new FileCredentialsImpl(CredentialsScope.GLOBAL,"docker-key.pem","Docker Engine client key", dfi,"","")

store.addCredentials(domain, caFile)
store.addCredentials(domain, certFile)
store.addCredentials(domain, keyFile)