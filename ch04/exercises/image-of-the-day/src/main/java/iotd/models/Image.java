package iotd;

public class Image {

    private String url;
    private String caption;
    private String copyright;

    public Image() {}

    public Image(String url, String caption, String copyright) {
        setUrl(url);
        setCaption(caption);
        setCopyright(copyright);
    }

    public String getUrl() {
    	return url;
    }
    
    public void setUrl(String url) {
        this.url = url;
    }

    public String getCaption() {
    	return caption;
    }
    
    public void setCaption(String caption) {
        this.caption = caption;
    }

    public String getCopyright() {
    	return copyright;
    }
    
    public void setCopyright(String copyright) {
        this.copyright = copyright;
    }

}
