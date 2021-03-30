import React from 'react';
import './Section.css';
import Subsection from '../Subsection/Subsection';
import { useRecoilState } from 'recoil';
import subsectionState from '../../../../recoilStates/subsectionState';

const Section = ({ name, subsections, clicked, id, showId }) => {
    const [selected, changeSubsection] = useRecoilState(subsectionState);
    let subs = subsections.sort((a, b) => a.order - b.order).map(subsection => {
        return (
            <Subsection
                key={subsection.id}
                selected={selected?.id === subsection.id}
                clicked={() => changeSubsection(subsection)} name={subsection.name} />
        );
    });

    return (
        <>
            <div onClick={clicked} className="Section"> {name} </div>
            {showId === id && subs !== [] ? <div className='Subsections'>{subs}</div> : null}
        </>
    );
}

export default Section;