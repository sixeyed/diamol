package iotd;

public class Config {

    private String environment;
    private String apodUrl;

    public Config() {}

    public Config(String environment, String apodUrl) {
        setEnvironment(environment);
        setApodUrl(apodUrl);
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
