package iotd;

import com.fasterxml.jackson.annotation.JsonProperty;

public class ApodImage {

    private String url;
    private String title;
    private String copyright;
    @JsonProperty("media_type")
    private String mediaType;

    public String getUrl() {
    	return url;
    }
    
    public void setUrl(String url) {
        this.url = url;
    }

    public String getTitle() {
    	return title;
    }
    
    public void setTitle(String title) {
        this.title = title;
    }

    public String getCopyright() {
    	return copyright;
    }
    
    public void setCopyright(String copyright) {
        this.copyright = copyright;
    }

    public String getMediaType() {
    	return mediaType;
    }
    
    public void setMediaType(String mediaType) {
        this.mediaType = mediaType;
    }}
