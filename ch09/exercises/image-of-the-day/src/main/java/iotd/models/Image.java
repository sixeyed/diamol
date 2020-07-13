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
        // if it's a YouTube link need to format the URL to get the thumbnail:
        // https://www.youtube.com/embed/ts0Ek3nLHew?rel=0 -> 
        // https://img.youtube.com/vi/ts0Ek3nLHew/0.jpg
        if (url.startsWith("https://www.youtube.com/embed/")) {
            url = "https://img.youtube.com/vi/" + url.substring(30, url.length()-6) + "/0.jpg";
        }
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
