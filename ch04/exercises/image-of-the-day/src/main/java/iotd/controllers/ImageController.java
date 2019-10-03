package iotd;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.client.RestTemplate;

@RestController
public class ImageController {

    private static final Logger log = LoggerFactory.getLogger(ImageController.class);

	@Autowired
	CacheService cacheService;
    
    @Value("${apod.url}")
	private String apodUrl;
    
    @Value("${apod.key}")
	private String apodKey;

    @RequestMapping("/image")
    public Image get() {
        log.debug("** GET /image called"); 

        Image img = cacheService.getImage();
        if (img == null) {
            RestTemplate restTemplate = new RestTemplate();
            ApodImage result = restTemplate.getForObject(apodUrl+apodKey, ApodImage.class);
            log.info("Fetched new APOD image from NASA"); 
            img = new Image(result.getUrl(), result.getTitle(), result.getCopyright());            
            cacheService.putImage(img);
        }
        else {
            log.debug("Loaded APOD image from cache"); 
        }
        return img;
    }
}
