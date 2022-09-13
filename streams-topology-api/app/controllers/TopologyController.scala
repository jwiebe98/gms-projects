package controllers

import com.google.inject.Inject
import kafka.KafkaMap
import play.api.mvc.{Action, AnyContent, BaseController, ControllerComponents}

import javax.inject.Singleton

@Singleton
class TopologyController @Inject()(val controllerComponents: ControllerComponents)
  extends BaseController {

  val topologies = new KafkaMap[String, String]("topologies")

  def getByApplicationId(applicationId: String): Action[AnyContent] = Action {
    topologies.get(applicationId) match {
      case Some(topology) => Ok(topology)
      case None => NoContent
    }
  }

  def getRegisteredApplications: Action[AnyContent] = Action {
    Ok(topologies.getKeys.mkString("[", ", ", "]"))
  }

}


