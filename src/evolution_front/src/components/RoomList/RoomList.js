import React, { useEffect } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { useNavigate } from "react-router-dom";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCalendar, faCheckCircle, faEllipsis, faSearch, faUser, faArrowLeft } from '@fortawesome/free-solid-svg-icons'
import "./RoomList.css";
import plant_img from '../../img/plant.jpg';
import jellyfish_img from '../../img/jellyfish.jpg';
import pterodactyl_img from '../../img/pterodactyl.jpg';

import { roomActions } from "../../actions";

const RoomList = (props) => {
  let navigation = useNavigate();

  useEffect(() => {
    props.list();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleBack = (_) => {
    navigation('/menu');
  };

  const addAdditionIcon = (additionType) => {
    const iconSize = 30;
    if (additionType === 'plant') {
      return <img className="rounded-circle" src={plant_img} alt="plant" width={iconSize} height={iconSize} />;
    }
    else if (additionType === 'timeToFly') {
      return <img className="rounded-circle" src={pterodactyl_img} alt="pterodactyl" width={iconSize} height={iconSize} />;
    }
    else if (additionType === 'continents') {
      return <img className="rounded-circle" src={jellyfish_img} alt="jellyfish" width={iconSize} height={iconSize} />;
    }
    else {
      return "";
    }
  };

  const showRooms = () => {
    let rooms = props.roomState.rooms;
    if (rooms === undefined) {
      return "";
    }
    return (
      <ul className="list list-inline">
        {rooms.map((room) => {
          return (
            <li className="d-flex justify-content-between" key={room.name}>
              <div className="d-flex flex-row align-items-center">
                <FontAwesomeIcon icon={faCheckCircle} className="text-success" />
                <div className="ms-2">
                  <h4 className="mb-0">{room.name}</h4>
                  <div className="d-flex flex-row mt-1 text-black-50 date-time">
                    <div>
                      <FontAwesomeIcon icon={faUser} />
                      <span className="h6 ms-2">{room.inGameUsers.find(x => x.isHost).user.userName}</span>
                    </div>
                  </div>
                </div>
              </div>
              <div className="d-flex flex-row align-items-center">Количество карт: {room.numOfCards}</div>
              <div className="d-flex flex-row align-items-center">
                <div className="d-flex flex-column me-2">
                  <div className="profile-image">
                    {addAdditionIcon('plant')}
                    {addAdditionIcon('timeToFly')}
                    {addAdditionIcon('continents')}
                  </div>
                  <span className="date-time">
                    <FontAwesomeIcon icon={faCalendar} className="text-secondary me-1" />
                    {new Date(room.createdDateTime).toLocaleString()}
                  </span>
                </div>
                <div className="ms-5">
                  <span className="btn btn-success">Присоединиться</span>
                </div>
              </div>
            </li>
          );
        })}
      </ul>
    );
  };

  return (
    <div className="room-list-window">
      <div className="list-rooms">
        <div className="d-flex justify-content-between align-items-center activity">
          <div className="icons"><FontAwesomeIcon icon={faArrowLeft} onClick={handleBack} /></div>
          <div className="list-title"><span className="activity-done">Список игр</span></div>
          <div className="icons"><FontAwesomeIcon icon={faSearch} className="me-3" /><FontAwesomeIcon icon={faEllipsis} /></div>
        </div>
        {showRooms()}
      </div>
    </div>
  );
};

const mapState = (state) => {
  return state;
};

const mapDispatchToProps = (dispatch) => {
  return {
    list: bindActionCreators(roomActions.list, dispatch),
  };
};

export default connect(mapState, mapDispatchToProps)(RoomList);
