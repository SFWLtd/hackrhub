package com.sfwltd.lineup.util

import org.glassfish.jersey.server.spi.internal.ValueFactoryProvider
import org.slf4j.Logger
import org.slf4j.LoggerFactory
import java.io.IOException
import java.util.regex.Pattern
import javax.annotation.Priority
import javax.servlet.*
import javax.ws.rs.HttpMethod
import javax.ws.rs.Priorities
import javax.ws.rs.container.ContainerRequestContext
import javax.ws.rs.container.ContainerRequestFilter
import javax.ws.rs.ext.Provider


/**
 * Created by SKX on 08/03/2017.
 */
@Priority(Priorities.AUTHENTICATION)
class URLMappedFilter : ContainerRequestFilter {
    private val logger: Logger = LoggerFactory.getLogger(URLMappedFilter::class.java)
    private val allowPattern: Pattern
    private val wrappedFilter: ContainerRequestFilter

    constructor(wrappedFilterP: ContainerRequestFilter, allowPatternStr: String){
        logger.debug("URLMappedFilter initialised "+allowPatternStr)
        allowPattern= Pattern.compile(allowPatternStr)
        wrappedFilter=wrappedFilterP
    }

    override fun filter(requestContext: ContainerRequestContext?) {
        if(requestContext!=null && requestContext.method!=HttpMethod.OPTIONS ) {
            logger.debug("URLMappedFilter called checking pattern " + allowPattern.pattern() + " and uri " + requestContext.uriInfo.path)
            if (allowPattern.matcher(requestContext.uriInfo.path).matches()) {
                logger.debug("Yay matched! ")
                wrappedFilter.filter(requestContext)
            }
        }
    }

}