package iotd;

import java.util.*; 
import java.util.stream.*;

import io.micrometer.core.annotation.Timed;
import io.micrometer.core.instrument.MeterRegistry;

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

    @Autowired
    MeterRegistry registry;
    
    @Value("${apod.url}")
	private String apodUrl;
    
    @Value("${apod.key}")
	private String apodKey;

    @RequestMapping("/image")
    @Timed()
    public Image get() {
        log.debug("** GET /image called"); 

        Image img = cacheService.getImage();
        if (img == null) {
            RestTemplate restTemplate = new RestTemplate();
            ApodImage[] result = restTemplate.getForObject(apodUrl+apodKey, ApodImage[].class);
            log.info("Fetched new APOD images from NASA");
            ApodImage match = Arrays.stream(result)
                                    .filter(x -> "image".equals(x.getMediaType()))
                                    .findFirst()
                                    .get();

            log.info("Fetched new APOD image from NASA"); 
            registry.counter("iotd_api_image_load", "status", "success").increment();

            img = new Image(match.getUrl(), match.getTitle(), match.getCopyright());          
            cacheService.putImage(img);
        }
        else {
            log.debug("Loaded APOD image from cache");             
            registry.counter("iotd_api_image_load", "status", "cached").increment();
        }
        return img;
    }
}
