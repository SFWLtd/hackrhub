package com.sfwltd.lineup.util

import io.dropwizard.logging.LoggingFactory
import org.slf4j.LoggerFactory
import java.io.PrintWriter
import java.io.StringWriter
import javax.ws.rs.WebApplicationException
import javax.ws.rs.ext.ExceptionMapper
import javax.ws.rs.ext.Provider
import javax.ws.rs.core.Response


/**
 * Created by SKX on 21/03/2017.
 */
@Provider
class RestExceptionMapper : ExceptionMapper<Throwable> {

    override fun toResponse(exception: Throwable): Response {
        log.error("toResponse() caught exception", exception)

        return Response.status(getStatusCode(exception))
                .entity(getEntity(exception))
                .build()
    }

    /*
     * Get appropriate HTTP status code for an exception.
     */
    private fun getStatusCode(exception: Throwable): Int {
        if (exception is WebApplicationException) {
            return exception.response.status
        }

        return Response.Status.INTERNAL_SERVER_ERROR.getStatusCode()
    }

    /*
     * Get response body for an exception.
     */
    private fun getEntity(exception: Throwable): Any {
        // return stack trace for debugging (probably don't want this in prod...)
        val errorMsg = StringWriter()
        exception.printStackTrace(PrintWriter(errorMsg))
        return errorMsg.toString()
    }

    companion object {
        private val log = LoggerFactory.getLogger(RestExceptionMapper::class.java)
    }
}