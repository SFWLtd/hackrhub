package com.civica.hackrhub.auth

import io.dropwizard.auth.Authorizer

/**
 * Created by SKX on 04/05/2017.
 */
class HackrhubAuthorizer : Authorizer<UserPrincipal> {
    override fun authorize(principal: UserPrincipal?, role: String?): Boolean {
        TODO("not implemented") //To change body of created functions use File | Settings | File Templates.
    }
}