package iotd;

public class Config {

    private String release;
    private String environment;
    private String apodUrl;

    public Config() {}

    public Config(String release, String environment, String apodUrl) {
        setRelease(release);
        setEnvironment(environment);
        setApodUrl(apodUrl);
    }

    public String getRelease() {
    	return release;
    }
    
    public void setRelease(String release) {
        this.release = release;
    }
    
    public String getEnvironment() {
    	return environment;
    }
    
    public void setEnvironment(String environment) {
        this.environment = environment;
    }

    public String getApodUrl() {
    	return apodUrl;
    }
    
    public void setApodUrl(String apodUrl) {
        this.apodUrl = apodUrl;
    }
}
