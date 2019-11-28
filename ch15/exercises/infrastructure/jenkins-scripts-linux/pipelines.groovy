import jenkins.*
import jenkins.model.*
import hudson.*
import hudson.model.*

import hudson.plugins.git.*;
import hudson.triggers.SCMTrigger;
import org.jenkinsci.plugins.workflow.job.WorkflowJob;
import org.jenkinsci.plugins.workflow.cps.CpsScmFlowDefinition;

def gitUser = "diamol"
def gitRepo = "diamol"
def gitUrl = "http://gogs:3000/${gitUser}/${gitRepo}.git"

def jenkins = Jenkins.instance;

def scm = new GitSCM(gitUrl)
scm.branches = [new BranchSpec("*/master")];
def workflowJob = new WorkflowJob(jenkins, "${gitRepo}");
workflowJob.definition = new CpsScmFlowDefinition(scm, "ch15/exercises/Jenkinsfile");
def gitTrigger = new SCMTrigger("* * * * *");
workflowJob.addTrigger(gitTrigger);
workflowJob.save();

jenkins.reload()